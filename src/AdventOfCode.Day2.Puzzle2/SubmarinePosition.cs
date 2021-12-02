using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2.Puzzle2;

public struct SubmarinePosition
{
    public SubmarinePosition(int x, int depth, int aim)
    {
        X = x;
        Depth = depth;
        Aim = aim;
    }

    public int Aim { get; }
    public int Depth { get; }
    public int X { get; }

    public SubmarinePosition Down(int magnitude)
    {
        return new SubmarinePosition(X, Depth, Aim + magnitude);
    }

    public SubmarinePosition Forward(int magnitude)
    {
        var newX = X + magnitude;
        var newDepth = Depth + (Aim * magnitude);

        return new SubmarinePosition(newX, newDepth, Aim);
    }

    public SubmarinePosition Up(int magnitude)
    {
        return new SubmarinePosition(X, Depth, Aim - magnitude);
    }

    public SubmarinePosition Transform(SubmarineCommandDirection direction, int magnitude)
    {
        return direction switch
        {
            SubmarineCommandDirection.Forward => Forward(magnitude),
            SubmarineCommandDirection.Down => Down(magnitude),
            SubmarineCommandDirection.Up => Up(magnitude),
            _ => throw new InvalidOperationException($"'{nameof(direction)}' has invalid value: {direction}")
        };
    }

    public override string ToString()
    {
        return $"{nameof(X)}: {X}, {nameof(Depth)}: {Depth}, {nameof(Aim)}: {Aim}";
    }
}