namespace BounsingBall.Models;

public class Ball
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double VX { get; private set; }
    public double VY { get; private set; }
    public double Radius { get; private set; }
    public string Color { get; private set; }
    public double Mass { get; private set; }
    public Ball CollidePrev { get; private set; }

    public Ball(double x, double y, double xVel, double yVel, double radius, string color, double mass)
    {
        (X, Y, VX, VY, Radius, Color, Mass) = (x, y, xVel, yVel, radius, color, mass);
        this.CollidePrev = null;
    }

    public void Move(double width, double height, IList<Ball> others)
    {

        #region collision with other balls
        foreach (Ball b in others)
        {
            double d = Math.Sqrt(Math.Pow(this.X - b.X, 2) + Math.Pow(this.Y - b.Y, 2));
            if (this != b && this.CollidePrev == null && d <= this.Radius + b.Radius)
            {
                this.CollidePrev = b;
                b.CollidePrev = this;
                Collide(this, b);
            }
            else if (this.CollidePrev == b && d > this.Radius + b.Radius)
            {
                this.CollidePrev = null;
                b.CollidePrev = null;
            }
        }
        #endregion

        #region Check collision With side walls
        if (X <= Radius)
            VX = Math.Abs(VX);
        if (X >= width - Radius)
            VX = Math.Abs(VX) * (-1);

        if (Y <= Radius)
            VY = Math.Abs(VY);
        if (Y >= height - Radius)
            VY = Math.Abs(VY) * (-1);

        #endregion

        X += VX;
        Y += VY;
    }

    private void Collide(Ball b1, Ball b2)
    {
        double nx = b2.X - b1.X;
        double ny = b2.Y - b1.Y;
        double nLength = Math.Sqrt(Math.Pow(nx, 2) + Math.Pow(ny, 2));
        double unx = nx / nLength;
        double uny = ny / nLength;
        double utx = (-1) * uny;
        double uty = unx;
        // V1n = vector(Un) * vector(v1)
        // V1t = vector(Ut) * vector(v1)
        double v1n = unx * b1.VX + uny * b1.VY;
        double v1t = utx * b1.VX + uty * b1.VY;
        double v2n = unx * b2.VX + uny * b2.VY;
        double v2t = utx * b2.VX + uty * b2.VY;

        double vp1t = v1t;
        double vp2t = v2t;

        double vp1n = (v1n * (b1.Mass - b2.Mass) + 2 * b2.Mass * v2n) / (b1.Mass + b2.Mass);
        double vp2n = (v2n * (b2.Mass - b1.Mass) + 2 * b1.Mass * v1n) / (b1.Mass + b2.Mass);

        b1.VX = vp1n * unx + vp1t * utx;
        b1.VY = vp1n * uny + vp1t * uty;

        b2.VX = vp2n * unx + vp2t * utx;
        b2.VY = vp2n * uny + vp2t * uty;

    }

}