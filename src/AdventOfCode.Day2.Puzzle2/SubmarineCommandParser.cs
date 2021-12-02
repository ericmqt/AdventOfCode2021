using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2.Puzzle2;

public class SubmarineCommandParser : IPuzzleInputParser<SubmarineCommand>
{
    public SubmarineCommand Parse(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            throw new ArgumentException($"'{nameof(line)}' cannot be null or empty.", nameof(line));
        }

        // If the line contains no spaces, it is not in a valid format
        if (!line.Contains(' '))
        {
            throw new FormatException($"Unable to parse value as {nameof(SubmarineCommand)}: \"{line}\"");
        }

        // Direction and magnitude are separated by a space
        var lineParts = line.Split(' ');

        // A valid command splits into two parts exactly; if we do not have two parts we do not have a valid input
        if (lineParts.Length != 2)
        {
            throw new FormatException(
                $"Unable to parse value as {nameof(SubmarineCommand)}. Expected string containing one space character: \"{line}\"");
        }

        // Parse direction and magnitude
        var direction = ParseCommandDirection(lineParts[0]);

        var magnitudeString = lineParts[1];

        if (!int.TryParse(magnitudeString, out var magnitude)) // Pop, pop!
        {
            throw new FormatException($"Command magnitude is not in a valid format: \"{magnitudeString}\"");
        }

        return new SubmarineCommand(direction, magnitude);
    }

    /// <summary>
    /// Parses submarine commands like "forward 4" into a <see cref="SubmarineCommand"/> value.
    /// </summary>
    /// <param name="inputLine">A string representing the value of a line from the command input file.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"><paramref name="inputLine"/> is null or empty.</exception>
    /// <exception cref="FormatException"><paramref name="inputLine"/> is not in a valid format.</exception>
    private static SubmarineCommand ParseCommand(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            throw new ArgumentException($"'{nameof(inputLine)}' cannot be null or empty.", nameof(inputLine));
        }

        // If the line contains no spaces, it is not in a valid format
        if (!inputLine.Contains(' '))
        {
            throw new FormatException($"Unable to parse value as {nameof(SubmarineCommand)}: \"{inputLine}\"");
        }

        // Direction and magnitude are separated by a space
        var lineParts = inputLine.Split(' ');

        // A valid command splits into two parts exactly; if we do not have two parts we do not have a valid input
        if (lineParts.Length != 2)
        {
            throw new FormatException(
                $"Unable to parse value as {nameof(SubmarineCommand)}. Expected string containing one space character: \"{inputLine}\"");
        }

        // Parse direction and magnitude
        var direction = ParseCommandDirection(lineParts[0]);

        var magnitudeString = lineParts[1];

        if (!int.TryParse(magnitudeString, out var magnitude)) // Pop, pop!
        {
            throw new FormatException($"Command magnitude is not in a valid format: \"{magnitudeString}\"");
        }

        return new SubmarineCommand(direction, magnitude);
    }

    /// <summary>
    /// Parses submarine command direction strings "forward", "down", and "up" into <see cref="SubmarineCommandDirection"/>.
    /// </summary>
    /// <param name="commandName">The name of the command direction.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"><paramref name="commandName"/> is null or empty.</exception>
    /// <exception cref="FormatException"><paramref name="commandName"/> is not in a valid format.</exception>
    private static SubmarineCommandDirection ParseCommandDirection(string commandName)
    {
        if (string.IsNullOrEmpty(commandName))
        {
            throw new ArgumentException($"'{nameof(commandName)}' cannot be null or empty.", nameof(commandName));
        }

        if (commandName.Equals("forward", StringComparison.Ordinal))
        {
            return SubmarineCommandDirection.Forward;
        }
        else if (commandName.Equals("down", StringComparison.Ordinal))
        {
            return SubmarineCommandDirection.Down;
        }
        else if (commandName.Equals("up", StringComparison.Ordinal))
        {
            return SubmarineCommandDirection.Up;
        }

        throw new FormatException($"Unable to parse value as {nameof(SubmarineCommandDirection)}: \"{commandName}\"");
    }
}