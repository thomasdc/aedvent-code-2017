using System.Collections.Immutable;
using System.Linq;
using static System.Math;

static class Bridge
{
    public static int Strongest(ImmutableList<Component> components, int pins = 0, int strength = 0) 
        => (
            from component in components
            where component.Matches(pins)
            select Strongest(components.Remove(component), component.Other(pins), strength + component.Strength)
        ).Concat(new []{strength})
        .Max();

    public static (int strength, int length) Longest(IImmutableList<Component> components, int pins = 0, int strength = 0, int length = 0)
        => (
                from component in components
                where component.Matches(pins)
                select Longest(components.Remove(component), component.Other(pins), strength + component.Strength, length + 1)
            ).Concat(new[] {(strength: strength, length: length)})
            .OrderByDescending(x => x.length)
            .ThenByDescending(x => x.strength)
            .First();
}

struct Component
{
    public readonly int Port1;
    public readonly int Port2;
    public Component(int port1, int port2)
    {
        Port1 = port1;
        Port2 = port2;
    }

    public int Smallest => Min(Port1, Port2);
    public bool Matches(int pins) => pins == Port1 || pins == Port2;
    public int Other(int pins) => pins == Port1 ? Port2 : Port1;

    public int Strength => Port1 + Port2;

    public static Component Parse(string s)
    {
        var split = s.Split('/');
        return new Component(int.Parse(split[0]), int.Parse(split[1]));
    }
    public override string ToString()
    {
        return $"{Port1}/{Port2}";
    }
}