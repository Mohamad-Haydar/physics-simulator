using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.JSInterop;

namespace BounsingBall.Pages
{
    public partial class Home
    {
        private readonly Models.Field BallsField = new(10);
        private Canvas2DContext ctx;
        protected BECanvasComponent CanvasRef;
        // private DateTime LastRender;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            ctx = await CanvasRef.CreateCanvas2DAsync();
            await JsRuntime.InvokeAsync<object>("initRenderJS", DotNetObjectReference.Create(this));
            await base.OnInitializedAsync();
        }

        [JSInvokable]
        public void ResizeInBlazor(double width, double height) => BallsField.Resize(width, height);

        [JSInvokable]
        public async ValueTask RenderInBlazor()
        {
            if (BallsField.Balls.Count == 0)
                BallsField.AddRandomBalls();
            BallsField.StepForward();

            await ctx.BeginBatchAsync();
            await ctx.ClearRectAsync(0, 0, BallsField.Width, BallsField.Height);
            await ctx.SetFillStyleAsync("#fff");
            await ctx.FillRectAsync(0, 0, BallsField.Width, BallsField.Height);

            foreach (var ball in BallsField.Balls)
            {
                await ctx.BeginPathAsync();
                await ctx.ArcAsync(ball.X, ball.Y, ball.Radius, 0, 2 * Math.PI, false);
                await ctx.SetFillStyleAsync(ball.Color);
                await ctx.FillAsync();
                await ctx.StrokeAsync();
            }
            await ctx.EndBatchAsync();
        }
    }
}


// double fps = 1.0 / (DateTime.Now - LastRender).TotalSeconds;
// LastRender = DateTime.Now;

// await this.ctx.SetFontAsync("26px Segoe UI");
// await this.ctx.SetFillStyleAsync("#000");
// await this.ctx.FillTextAsync("Blazor WebAssembly + HTML Canvas", 10, 30);
// await this.ctx.SetFontAsync("16px consolas");
// await this.ctx.SetFillStyleAsync("red");
// await this.ctx.FillTextAsync("Blazor WebAssembly + HTML Canvas", 10, 50);
// await this.ctx.FillTextAsync($"FPS: {fps:0.000}", 10, 50);
// await this.ctx.SetStrokeStyleAsync("#000");