using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

public interface IPuzzleInputParser<T>
{
    T Parse(string line);
}