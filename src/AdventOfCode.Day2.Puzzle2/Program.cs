namespace AdventOfCode.Day2.Puzzle2;

class Program
{
    static async Task<int> Main(string[] args)
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

        // Keep track of submarine position
        var position = new SubmarinePosition(x: 0, depth: 0, aim: 0);

        // Parse commands
        var parser = new SubmarineCommandParser();

        await foreach (var command in PuzzleInputReader.EnumerateParsedLinesAsync(parser, inputFileName))
        {
            // Get a new submarine position by transforming the current position with command direction and magnitude
            var newPosition = position.Transform(command.Direction, command.Magnitude);

            Console.WriteLine($"({position}) -> {command.Direction} {command.Magnitude} -> {newPosition}");
            //WritePositionCommandTransform(command, position, newPosition);

            position = newPosition;
        }

        Console.WriteLine("Finished processing commands");

        // Calculate answer puzzle
        var result = position.X * position.Depth;

        Console.WriteLine($"Puzzle answer: {result}");

        return 0;
    }
}