using System;
using Xunit;

public class Specs
{
    [Fact]
    public void Test1()
    {
        var result = Spinlock.Find(3, 2017);
        Assert.Equal(638, result.buffer[result.index + 1]);
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal(1226, Spinlock.FindFast(3, 2017));
    }


}