namespace BounsingBall.Models2;

public class Ball
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double XVel { get; private set; }
    public double YVel { get; private set; }
    public double Radius { get; private set; }
    public string Color { get; private set; }

    public Ball(double x, double y, double xVel, double yVel, double radius, string color)
    {
        (X, Y, XVel, YVel, Radius, Color) = (x, y, xVel, yVel, radius, color);
    }

    public void Move(double width, double height)
    {
        if (X <= 0 + Radius || X >= width - Radius)
            XVel *= -1;
        if (Y <= 0 + Radius || Y >= height - Radius)
            YVel *= -1;

        X += XVel;
        Y += YVel;
    }
}
