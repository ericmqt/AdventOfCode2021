namespace AdventOfCode.Day4.Puzzle2;

class Program
{
    static int Main(string[] args)
    {
        // Validate args
        if (args.Length < 1)
        {
            Console.WriteLine("[error] Missing input filename argument");
            return -1;
        }

        var inputFileName = args[0];

        if (!File.Exists(inputFileName))
        {
            Console.WriteLine("[error] Input file does not exist or is inaccessible: \"{inputFileName}\"");
            return -2;
        }

        // Read number draws
        var numberDraws = BingoReader.ReadNumberDraws(inputFileName);
        var bingoBoards = BingoReader.ReadBingoBoards(inputFileName).ToList();

        var winningBoard = PlayBingo(numberDraws, bingoBoards, out var winningNumber);

        if (winningBoard is null)
        {
            Console.WriteLine($"[error] No winner found");
            return -3;
        }

        var puzzleAnswer = winningBoard.CalculateScore(winningNumber);
        Console.WriteLine($"Puzzle answer: {puzzleAnswer}");

        return 0;
    }

    static BingoBoard? PlayBingo(int[] numberDraws, IEnumerable<BingoBoard> bingoBoards, out int winningNumber)
    {
        if (numberDraws is null)
        {
            throw new ArgumentNullException(nameof(numberDraws));
        }

        if (bingoBoards is null)
        {
            throw new ArgumentNullException(nameof(bingoBoards));
        }

        var game = new BingoGame(bingoBoards, numberDraws);

        foreach (var num in numberDraws)
        {
            if (!game.PlayRound(num))
            {
                break;
            }

            if (game.PlayersRemaining == 0)
            {
                winningNumber = num;
                return game.WinningBoards.Last();
            }
        }

        winningNumber = -1;
        return null;
    }
}