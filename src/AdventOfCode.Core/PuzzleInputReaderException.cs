using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

[Serializable]
public class PuzzleInputReaderException : Exception
{
    public PuzzleInputReaderException() { }

    public PuzzleInputReaderException(string message, string fileName, int lineNumber)
        : this(message, fileName, lineNumber, innerException: null)
    {

    }

    public PuzzleInputReaderException(string message, string fileName, int lineNumber, Exception? innerException)
        : base(message, innerException)
    {
        // Set to empty string if null, don't bother with throwing ArgumentNullException while
        // trying to throw this exception
        FileName = !string.IsNullOrEmpty(fileName) ? fileName : string.Empty;

        LineNumber = lineNumber;
    }

    protected PuzzleInputReaderException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {

    }

    public string FileName { get; } = string.Empty;
    public int LineNumber { get; }
}