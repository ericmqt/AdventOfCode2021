using System.Runtime.CompilerServices;

namespace AdventOfCode;

public static class PuzzleInputReader
{
    /// <summary>
    /// Asynchronously enumerates the lines of a puzzle input file.
    /// </summary>
    /// <param name="fileName">The filename of the puzzle input file.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the enumeration.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"><paramref name="fileName"/> is null or empty.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="fileName"/> refers to a file that does not exist or is inaccessible.</exception>
    /// <exception cref="PuzzleInputReaderException">Thrown when encountering a null or empty input line.</exception>
    public static async IAsyncEnumerable<string> EnumerateLinesAsync(string fileName, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
        }

        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"File specified by '{nameof(fileName)}' does not exist or is inaccessible", fileName);
        }

        // Open the input file
        var inputStreamOptions = new FileStreamOptions()
        {
            Mode = FileMode.Open,
        };

        using var inputStream = new StreamReader(fileName, inputStreamOptions);

        // Keep track of line number for error reporting purposes
        int lineNumber = 1;

        while (!inputStream.EndOfStream)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }

            var inputLine = await inputStream.ReadLineAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(inputLine))
            {
                throw new PuzzleInputReaderException($"Line {lineNumber} of file is null or empty", fileName, lineNumber);
            }

            yield return inputLine;

            lineNumber++;
        }
    }

    /// <summary>
    /// Asynchronously parses each line of the puzzle input file using the specified parser.
    /// </summary>
    /// <typeparam name="T">The type of value to parse from each input line.</typeparam>
    /// <param name="parser">Instance of input line parser.</param>
    /// <param name="fileName">The filename of the puzzle input file.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the enumeration.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="parser"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="fileName"/> is null or empty.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="fileName"/> refers to a file that does not exist or is inaccessible.</exception>
    /// <exception cref="PuzzleInputReaderException">Thrown when encountering a null or empty input line.</exception>
    /// <exception cref="PuzzleInputParsingException">Thrown when the parser is unable to parse a value from an input line.</exception>
    public static async IAsyncEnumerable<T> EnumerateParsedLinesAsync<T>(IPuzzleInputParser<T> parser, string fileName, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (parser is null)
        {
            throw new ArgumentNullException(nameof(parser));
        }

        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
        }

        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"File specified by '{nameof(fileName)}' does not exist or is inaccessible", fileName);
        }

        // Keep track of line number for error reporting purposes
        int lineNumber = 1;

        await foreach (var inputLine in EnumerateLinesAsync(fileName, cancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }

            T parsedValue;

            try { parsedValue = parser.Parse(inputLine); }
            catch (Exception ex)
            {
                throw new PuzzleInputParsingException(
                    $"Unable to parse input line {lineNumber}. See inner exception for details.",
                    inputLine,
                    fileName,
                    lineNumber,
                    ex);
            }

            yield return parsedValue;

            lineNumber++;
        }
    }

    /*public static async Task<IList<T>> ParseLinesAsync<T>(IPuzzleInputParser<T> parser, string fileName, CancellationToken cancellationToken = default)
    {
        if (parser is null)
        {
            throw new ArgumentNullException(nameof(parser));
        }

        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
        }

        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"File specified by '{nameof(fileName)}' does not exist or is inaccessible", fileName);
        }

        // Keep track of line number for error reporting purposes
        int lineNumber = 1;

        await foreach (var inputLine in EnumerateLinesAsync(fileName, cancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }

            T parsedValue;

            try { parsedValue = parser.Parse(inputLine); }
            catch (Exception ex)
            {
                throw new PuzzleInputParsingException(
                    $"Unable to parse input line {lineNumber}. See inner exception for details.",
                    inputLine,
                    fileName,
                    lineNumber,
                    ex);
            }

            yield return parsedValue;

            lineNumber++;
        }
    }*/
}
