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

        // Read depths from input file
        var depthMeasurements = await ReadDepthMeasurementsFromFileAsync(inputFileName).ConfigureAwait(false);

        var depthWindows = GetMeasurementWindows(depthMeasurements, windowSize: 3);

        // Keep track of the number of times the depth window increases from the previous window
        int depthMeasurementIncreases = 0;

        var previousWindow = depthWindows.First();

        foreach (var depth in depthWindows.Skip(1))
        {
            if (depth > previousWindow)
            {
                depthMeasurementIncreases++;
            }

            previousWindow = depth;
        }

        Console.WriteLine($"Number of depth increases: {depthMeasurementIncreases}");
        return 0;
    }

    static IList<int> GetMeasurementWindows(IList<int> depthMeasurements, int windowSize)
    {
        if (depthMeasurements is null)
        {
            throw new ArgumentNullException(nameof(depthMeasurements));
        }

        if (windowSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(windowSize), windowSize, $"'{nameof(windowSize)}' must be greater than zero");
        }

        if (depthMeasurements.Count < windowSize)
        {
            return new List<int>();
        }

        var windows = new List<int>();

        for (int i = 0; i + windowSize <= depthMeasurements.Count; i++)
        {
            var windowDepthSum =
                depthMeasurements[i] +
                depthMeasurements[i + 1] +
                depthMeasurements[i + 2];

            windows.Add(windowDepthSum);
        }

        return windows;
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
    static async Task<IList<int>> ReadDepthMeasurementsFromFileAsync(string fileName, CancellationToken cancellationToken = default)
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