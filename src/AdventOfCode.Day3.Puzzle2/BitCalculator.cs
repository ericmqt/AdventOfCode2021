using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3.Puzzle2
{
    public static class BitCalculator
    {
        /// <summary>
        /// Calculates the least common bit at the specified index in a collection of numbers.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bitIndex"></param>
        /// <returns>Returns greater than <c>0</c> if the least common bit is 1, otherwise 0.</returns>
        public static uint CalculateLeastCommonBitForPosition(IList<int> source, int bitIndex)
        {
            uint mask = (1u << ((sizeof(uint) * 8) - 1)) >> bitIndex;

            int totalOnes = 0;
            int totalZeros = 0;

            for (int i = 0; i < source.Count; i++)
            {
                if ((source[i] & mask) != 0)
                {
                    totalOnes++;
                }
                else
                {

                    totalZeros++;
                }
            }

            if (totalZeros == totalOnes || totalOnes > totalZeros || totalOnes == 0)
            {
                return 0;
            }

            return 0xFFFFFFFF;
        }

        /// <summary>
        /// Calculates the most common bit at the specified index in a collection of numbers.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bitIndex"></param>
        /// <returns>Returns greater than <c>1</c> if the most common bit is <c>1</c>, otherwise 0.</returns>
        public static uint CalculateMostCommonBitForPosition(IList<int> source, int bitIndex)
        {
            uint mask = (1u << ((sizeof(uint) * 8) - 1)) >> bitIndex;

            int totalOnes = 0;
            int totalZeros = 0;

            for (int i = 0; i < source.Count; i++)
            {
                if ((source[i] & mask) != 0)
                {
                    totalOnes++;
                }
                else
                {
                    totalZeros++;
                }
            }

            if (totalOnes == totalZeros || totalOnes > totalZeros)
            {
                return 0xFFFFFFFF;
            }

            return 0;
        }
    }
}
