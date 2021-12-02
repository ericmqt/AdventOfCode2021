using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

[Serializable]
public class PuzzleInputParsingException : Exception
{
    public PuzzleInputParsingException() { }

    public PuzzleInputParsingException(string message, string value, string fileName, int lineNumber)
        : this(message, value, fileName, lineNumber, innerException: null) { }

    public PuzzleInputParsingException(string message, string value, string fileName, int lineNumber, Exception? innerException)
        : base(message, innerException)
    {
        Value = value;
        FileName = !string.IsNullOrEmpty(fileName) ? fileName : string.Empty;
        LineNumber = lineNumber;
    }

    protected PuzzleInputParsingException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public string FileName { get; } = string.Empty;
    public int LineNumber { get; }
    public string? Value { get; }
}