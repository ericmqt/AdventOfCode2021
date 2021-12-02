using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2.Puzzle2;

public struct SubmarineCommand
{
    public SubmarineCommand(SubmarineCommandDirection direction, int magnitude)
    {
        Direction = direction;
        Magnitude = magnitude;
    }

    public SubmarineCommandDirection Direction { get; }
    public int Magnitude { get; }
}