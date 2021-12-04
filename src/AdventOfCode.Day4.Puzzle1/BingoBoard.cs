using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4.Puzzle1
{
    public class BingoBoard
    {
        private readonly int[] _board;
        private readonly int[] _markedNumbers;

        public const int BoardWidth = 5;
        public const int BoardHeight = 5;

        public BingoBoard(int[] boardNumbers)
        {
            if (boardNumbers is null)
            {
                throw new ArgumentNullException(nameof(boardNumbers));
            }

            if (boardNumbers.Length != 25)
            {
                throw new ArgumentException($"'{nameof(boardNumbers)}' must have a length of 25");
            }

            _board = boardNumbers;

            _markedNumbers = new int[25];
            Array.Fill(_markedNumbers, -1);
        }

        public int CalculateScore(int lastNumberDrawn)
        {
            int unmarkedNumberSum = 0;

            for (int i = 0; i < _board.Length; i++)
            {
                if (_markedNumbers[i] < 0)
                {
                    unmarkedNumberSum += _board[i];
                }
            }

            return unmarkedNumberSum * lastNumberDrawn;
        }

        public bool MarkNumber(int drawnNumber)
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i] == drawnNumber)
                {
                    _markedNumbers[i] = drawnNumber;
                    return true;
                }
            }

            return false;
        }

        public bool IsWinner()
        {
            foreach (var row in EnumerateMarkedRows())
            {
                if (!row.Contains(-1))
                {
                    return true;
                }
            }

            foreach (var col in EnumerateMarkedColumns())
            {
                if (!col.Contains(-1))
                {
                    return true;
                }
            }

            return false;
        }

        public int SumUnmarkedNumbers()
        {
            int result = 0;

            for (int i = 0; i < _board.Length; i++)
            {
                if (_markedNumbers[i] < 0)
                {
                    result += _board[i];
                }
            }

            return result;
        }

        private IEnumerable<int[]> EnumerateMarkedColumns()
        {
            for (int c = 0; c < 5; c++)
            {
                var col = new int[5]
                {
                    _markedNumbers[c + 0],
                    _markedNumbers[c + 5],
                    _markedNumbers[c + 10],
                    _markedNumbers[c + 15],
                    _markedNumbers[c + 20],
                };

                yield return col;
            }
        }

        private IEnumerable<int[]> EnumerateMarkedRows()
        {
            for (int r = 0; r < 5; r++)
            {
                var row = new int[5];

                _markedNumbers.AsSpan().Slice(r * 5, 5).CopyTo(row);

                yield return row;
            }
        }
    }
}
