using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4.Puzzle2
{
    public static class BingoReader
    {
        public static int[] ReadNumberDraws(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File specified by '{nameof(fileName)}' does not exist or is inaccessible", fileName);
            }

            using var inputStream = new StreamReader(fileName);

            var numberDrawLine = inputStream.ReadLine();

            if (string.IsNullOrEmpty(numberDrawLine))
            {
                throw new Exception("First line of input file is empty or null");
            }

            var numberDrawParts = numberDrawLine.Split(',');

            var numbers = new int[numberDrawParts.Length];

            for (int i = 0; i < numberDrawParts.Length; i++)
            {
                var nPart = numberDrawParts[i];

                if (!int.TryParse(nPart, out numbers[i]))
                {
                    throw new FormatException($"Unable to parse \"{nPart}\" as {nameof(Int32)}");
                }
            }

            return numbers;
        }

        public static IEnumerable<BingoBoard> ReadBingoBoards(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File specified by '{nameof(fileName)}' does not exist or is inaccessible", fileName);
            }

            using var inputStream = new StreamReader(fileName);

            // Skip empty line
            inputStream.ReadLine();
            

            while(!inputStream.EndOfStream)
            {
                // skip empty line
                inputStream.ReadLine();

                var board = ReadBingoBoard(inputStream);

                yield return board;
            }
        }

        private static BingoBoard ReadBingoBoard(StreamReader inputStream)
        {
            var boardNums = new int[25];

            for (int i = 0; i < 5; i++)
            {
                var numLine = inputStream.ReadLine();

                if (string.IsNullOrEmpty(numLine))
                {
                    throw new FormatException($"Line is empty but expected bingo numbers");
                }

                var numLineParts = numLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < 5; j++)
                {
                    int numIdx = (i * 5) + j;

                    if (!int.TryParse(numLineParts[j], out boardNums[numIdx]))
                    {
                        throw new FormatException($"Unable to parse board number \"{numLineParts[j]}\" as {nameof(Int32)}");
                    }
                }
            }

            return new BingoBoard(boardNums);
        }
    }
}
