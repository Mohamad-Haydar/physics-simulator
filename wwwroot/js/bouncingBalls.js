function renderJS() {
    theInstance.invokeMethodAsync('RenderInBlazor');
    window.requestAnimationFrame(renderJS);
}

function resizeCanvasToFitWindow() {
    var holder = document.getElementById('canvasHolder');
    var canvas = holder.querySelector('canvas');
    if (canvas) {
        canvas.width = 300;
        canvas.height = 200;
        theInstance.invokeMethodAsync('ResizeInBlazor', canvas.width, canvas.height);
    }
}

window.initRenderJS = (instance) => {
    window.theInstance = instance;
    window.addEventListener("resize", resizeCanvasToFitWindow);
    resizeCanvasToFitWindow();
    window.requestAnimationFrame(renderJS);
};