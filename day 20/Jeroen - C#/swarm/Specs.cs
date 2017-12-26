using System.Linq;
using Xunit;
using static System.Math;

struct Particle
{
    public readonly (double x, double y, double z) Position;
    public readonly (double x, double y, double z) Velocity;
    public readonly (double x, double y, double z) Acceleration;

    public Particle((double x, double y, double z) position, (double x, double y, double z) velocity, (double x, double y, double z) acceleration)
    {
        Position = position;
        Velocity = velocity;
        Acceleration = acceleration;
    }

    public Particle Tick()
    {
        var velocity = (x: Velocity.x + Acceleration.x, y: Velocity.y + Acceleration.y, z: Velocity.z + Acceleration.z);
        var position = (Position.x + velocity.x, Position.y + velocity.y, Position.z + velocity.z);
        return new Particle(position, velocity, Acceleration);
    }

    // pos(t) = p + vt + t(t+1)/2*a
    public (double x, double y, double z) GetPosition(double time)
    {
        return (
            calc(Position.x, Velocity.x, Acceleration.x, time),
            calc(Position.y, Velocity.y, Acceleration.y, time),
            calc(Position.z, Velocity.z, Acceleration.z, time)
            );
    }

    private double calc(double p, double v, double a, double time) => p + v * time + time * (time + 1) / 2 * a;

    public double Distance => Abs(Position.x) + Abs(Position.y) + Abs(Position.z);

    public static Particle Parse(string s)
    {
        var parts = (
            from part in s.Split(", ")
            let vector = part.Split("=")
            let name = vector[0]
            let values = vector[1].Trim(' ', '<', '>').Split(',').Select(int.Parse).ToArray()
            select (name: name, values: values)
        ).ToDictionary(x => x.name, x => (x.values[0], x.values[1], x.values[2]));
        return new Particle(parts["p"], parts["v"], parts["a"]);
    }

}

// pos(t) = p + vt + t(t+1)/2*a
public class Specs
{
    string input = "p=< 1,2,3>, v=< 2,3,4>, a=<-1,1,2>";

    [Fact]
    public void Particle_Parse()
    {
        var particle = Particle.Parse(input);
        Assert.Equal((1, 2, 3), particle.Position);
        Assert.Equal((2, 3, 4), particle.Velocity);
        Assert.Equal((-1, 1, 2), particle.Acceleration);
    }
    [Fact]
    public void Particle_Tick()
    {
        var particle = Particle.Parse(input);
        particle = particle.Tick();
        Assert.Equal((-1, 1, 2), particle.Acceleration);
        Assert.Equal((1, 4, 6), particle.Velocity);
        Assert.Equal((2, 6, 9), particle.Position);
    }

    [Fact]
    public void Test()
    {
        var g = new[]
        {
            (1, 2, 3),
            (1, 2, 3),
        }.GroupBy(x => x);
        Assert.Single(g);
    }

}

