using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3.Puzzle1
{
    public class DiagnosticCodeParser : IPuzzleInputParser<int>
    {
        public int Parse(string line)
        {
            var lineSpan = line.AsSpan();
            var lineLength = lineSpan.Length;

            int iVal = 0;
            int mask = 1 << (lineSpan.Length - 1);

            for (int i = 0; i < lineLength; i++)
            {
                var c = lineSpan[i];

                if (c != '0' && c != '1')
                {
                    throw new FormatException($"Input contains invalid character '{c}'");
                }

                if (c == '1')
                {
                    iVal |= mask;
                }

                mask >>= 1;
            }

            return iVal;
        }
    }
}
