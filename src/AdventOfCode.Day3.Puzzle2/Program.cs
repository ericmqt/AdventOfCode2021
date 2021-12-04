namespace AdventOfCode.Day3.Puzzle2;

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

        // Read input
        var diagnosticCodes = new List<int>();

        var diagParser = new DiagnosticCodeParser();

        await foreach (var parsedCode in PuzzleInputReader.EnumerateParsedLinesAsync(diagParser, inputFileName))
        {
            diagnosticCodes.Add(parsedCode);
        }

        // Calculate O2 generator rating using most-common bit filter
        if (!CalculateEquipmentRating(diagnosticCodes, BitCalculator.CalculateMostCommonBitForPosition, out var oxygenGeneratorRating))
        {
            Console.WriteLine($"[error] Oxygen generator rating calculation failed");
            return -3;
        }

        Console.WriteLine($"Oxygen generator rating: {oxygenGeneratorRating}");

        // Calculate CO2 scrubber rating using least-common bit filter
        if (!CalculateEquipmentRating(diagnosticCodes, BitCalculator.CalculateLeastCommonBitForPosition, out var co2ScrubberRating))
        {
            Console.WriteLine($"[error] CO2 scrubber rating calculation failed");
            return -4;
        }

        Console.WriteLine($"CO2 scrubber rating: {co2ScrubberRating}");

        var puzzleAnswer = oxygenGeneratorRating * co2ScrubberRating;

        Console.WriteLine($"Puzzle answer: {puzzleAnswer}");

        return 0;
    }

    static bool CalculateEquipmentRating(IList<int> diagnosticCodes, Func<IList<int>, int, uint> bitFilter, out int rating)
    {
        IList<int> ratingCodes = diagnosticCodes;

        for (int i = 0; i < sizeof(uint) * 8; i++)
        {
            // Calculate per-iteration filter bit
            var filterBits = bitFilter(ratingCodes, i);

            // Filter remaining rating codes using this iteration's filter bit
            ratingCodes = FilterDiagnosticCodes(ratingCodes, filterBits, i);

            if (ratingCodes.Count == 1)
            {
                rating = ratingCodes.First();
                return true;
            }
        }

        rating = -1;
        return false;
    }

    static IList<int> FilterDiagnosticCodes(IList<int> diagnosticCodes, uint gamma, int bitPosition)
    {
        var includedValues = new List<int>();

        uint mask = (1u << ((sizeof(uint) * 8) - 1)) >> bitPosition;

        for (int i = 0; i < diagnosticCodes.Count; i++)
        {
            var code = diagnosticCodes[i];

            if ((gamma & mask) == 0)
            {
                // Bit criteria is '0', per gamma, so negate gamma to change our criteria to '1' and
                // then check if the negated diagnostic code is also 1.

                if ((~code & mask) == (~gamma & mask))
                {
                    includedValues.Add(code);
                }
            }
            else
            {
                // Bit criteria is '1' so bitmask this digit and check if the result is non-zero
                if ((code & mask) != 0)
                {
                    includedValues.Add(code);
                }
            }
        }

        return includedValues;
    }
}