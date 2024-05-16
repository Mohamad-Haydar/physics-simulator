namespace BounsingBall.Models;

public class Field
{
    public double Width { get; set; }
    public double Height { get; set; }
    public int NbBalls { get; set; }
    public IList<Ball> Balls { get; set; }

    public Field(int nbBalls)
    {
        this.NbBalls = nbBalls;
        Balls = [];
    }

    public void Resize(double width, double height)
    {
        this.Width = width;
        this.Height = height;
    }

    public void StepForward()
    {
        foreach (var ball in Balls)
        {
            // Balls.Remove(ball);
            ball.Move(Width, Height, Balls);
            // Balls.Add(ball);
        }
    }

    public void AddRandomBalls()
    {
        double minSpeed = 1;
        double maxSpeed = 5;
        double radius = 7;
        Random rand = new();

        for (int i = 0; i < NbBalls; i++)
        {
            Balls.Add(
                new Ball(
                    x: Width * rand.NextDouble(),
                    y: Height * rand.NextDouble(),
                    xVel: RandomVelocity(rand, minSpeed, maxSpeed),
                    yVel: RandomVelocity(rand, minSpeed, maxSpeed),
                    radius: radius,
                    color: RandomColor(rand),
                    mass: 2
                )
            );
        }
    }

    private double RandomVelocity(Random rand, double min, double max)
    {
        double v = min + (max - min) * rand.NextDouble();
        while (v == 0)
        {
            v = min + (max - min) * rand.NextDouble();
        }
        if (rand.NextDouble() > .5)
            v *= -1;
        return v;
    }


    private string RandomColor(Random rand) => string.Format("#{0:X6}", rand.Next(0xFFFFFF));

}