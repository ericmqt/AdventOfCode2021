using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4.Puzzle2
{
    public class BingoGame
    {
        private readonly List<BingoBoard> _bingoBoards;
        private readonly int[] _numberDrawSource;
        private int _currentDrawIndex;
        private readonly List<BingoBoard> _winningBoards;

        public BingoGame(IEnumerable<BingoBoard> bingoBoards, int[] numberDrawSource)
        {
            _bingoBoards = bingoBoards != null
                ? bingoBoards.ToList()
                : throw new ArgumentNullException(nameof(bingoBoards));

            _numberDrawSource = numberDrawSource ?? throw new ArgumentNullException(nameof(numberDrawSource));

            _currentDrawIndex = -1;
            _winningBoards = new List<BingoBoard>();
        }

        public int PlayersRemaining => _bingoBoards.Count;
        public IEnumerable<BingoBoard> WinningBoards => _winningBoards;

        public bool PlayRound(int drawnNumber)
        {
            if (!_bingoBoards.Any())
            {
                return false;
            }

            var winningBoardIndices = new List<int>();

            for (int i = 0; i < _bingoBoards.Count; i++)
            {
                var board = _bingoBoards[i];

                board.MarkNumber(drawnNumber);

                if (board.IsWinner())
                {
                    winningBoardIndices.Add(i);
                    _winningBoards.Add(board);
                }
            }

            winningBoardIndices.Reverse();
            foreach (var winningBoardIndex in winningBoardIndices)
            {
                _bingoBoards.RemoveAt(winningBoardIndex);
            }

            return true;
        }
    }
}
