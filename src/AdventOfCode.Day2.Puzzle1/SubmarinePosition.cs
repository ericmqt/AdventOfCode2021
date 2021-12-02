using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2.Puzzle1
{
    public struct SubmarinePosition
    {
        public SubmarinePosition(int x, int depth)
        {
            X = x;
            Depth = depth;
        }

        public int Depth { get; }
        public int X { get; }

        public SubmarinePosition Down(int magnitude)
        {
            return new SubmarinePosition(X, Depth + magnitude);
        }

        public SubmarinePosition Forward(int magnitude)
        {
            return new SubmarinePosition(X + magnitude, Depth);
        }

        public SubmarinePosition Up(int magnitude)
        {
            return new SubmarinePosition(X, Depth - magnitude);
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
    }
}
