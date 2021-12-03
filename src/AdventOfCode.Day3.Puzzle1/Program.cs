namespace AdventOfCode.Day3.Puzzle1;

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

        // Calculate answer
        CalculateGammaAndEpsilon(diagnosticCodes, out var gamma, out var epsilon);

        Console.WriteLine($"Gamma: {gamma}");
        Console.WriteLine($"Epsilon: {epsilon}");

        var powerConsumption = gamma * epsilon;

        Console.WriteLine($"Power consumption: {powerConsumption} (gamma * epsilon)");

        return 0;
    }

    static void CalculateGammaAndEpsilon(IList<int> diagnosticCodes, out uint gamma, out uint epsilon)
    {
        uint mask = 1u << ((sizeof(uint) * 8) - 1);

        uint mostCommonBitsValue = 0;
        uint leastCommonBitsValue = 0;

        for (int i = 0; i < (sizeof(uint) * 8); i++)
        {
            int totalOnes = 0;
            int totalZeros = 0;

            for (int j = 0; j < diagnosticCodes.Count; j++)
            {
                if ((diagnosticCodes[j] & mask) != 0)
                {
                    totalOnes++;
                }
                else
                {
                    totalZeros++;
                }
            }

            uint mcb = totalOnes > totalZeros ? 0xFFFFFFFF : 0;

            /*
             * Set the least common bit pattern to the negated most-common bit pattern IF AND ONLY IF we found at
             * least one '1' bit; otherwise the negation of a most-common '0' bit gives a least-common bit pattern
             * for '1' even though no '1' bits were ever encountered, therefore '0' is the least AND most common bit.
             */

            uint lcb = totalOnes > 0 ? ~mcb : 0;

            mostCommonBitsValue = (mostCommonBitsValue & ~mask) | (mcb & mask);
            leastCommonBitsValue = (leastCommonBitsValue & ~mask) | (lcb & mask);

            // Shift bitmask for next digit
            mask = mask >> 1;
        }

        gamma = mostCommonBitsValue;
        epsilon = leastCommonBitsValue;
    }
}