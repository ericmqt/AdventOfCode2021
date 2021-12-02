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

        // Read depths and count the number of times the measurement has *increased* from the previous measurement
        var depthMeasurements = await ReadDepthMeasurementsFromFileAsync(inputFileName).ConfigureAwait(false);

        if (!depthMeasurements.Any())
        {
            Console.WriteLine("[error] Input file does not contain any depth measurements");

            return -3;
        }

        Console.WriteLine("Calculating the number of depth increases from one measurement to the next...");
        Console.WriteLine($"Total measurements: {depthMeasurements.Count()}");

        // Keep track of the number of times the depth measurement increases from the previous measurement
        int depthMeasurementIncreases = 0;

        // There is no measurement before the first measurement, so store the first measurement as the "previous" measurement
        // and skip the first element when calculating the number of depth increases
        int previousDepth = depthMeasurements.First();

        foreach (var depth in depthMeasurements.Skip(1))
        {
            if (depth > previousDepth)
            {
                depthMeasurementIncreases++;
            }

            previousDepth = depth;
        }

        Console.WriteLine($"Number of depth increases: {depthMeasurementIncreases}");
        return 0;
    }

    /// <summary>
    /// Reads a text file containing puzzle input as a sequence of integers.
    /// </summary>
    /// <param name="fileName">The filename of a text file containing puzzle input as provided by Advent of Code.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to use for canceling the read operation.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"><paramref name="fileName"/> is null or empty.</exception>
    /// <exception cref="FileNotFoundException">File specified by <paramref name="fileName"/> does not exist or is not accessible.</exception>
    /// <exception cref="FormatException">A line of the input file could not be parsed as <see cref="Int32"/>.</exception>
    static async Task<IEnumerable<int>> ReadDepthMeasurementsFromFileAsync(string fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
        }

        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"The file specified by argument '{nameof(fileName)}' does not exist or is not accessible", fileName);
        }

        // Collection to store results
        var depthMeasurements = new List<int>();

        // Open the input file
        using var inputStream = new StreamReader(fileName);

        // Keep track of the current line so the line number can be reported on a parsing error
        int lineNumber = 1;

        while (!inputStream.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Read the input line and parse it, or throw if parsing fails
            var inputLine = await inputStream.ReadLineAsync().ConfigureAwait(false);

            if (int.TryParse(inputLine, out var depth))
            {
                depthMeasurements.Add(depth);
            }
            else
            {
                throw new FormatException($"Input line number {lineNumber} could not be parsed as {nameof(Int32)}");
            }

            lineNumber++;
        }

        return depthMeasurements;
    }
}