using System;
using System.Collections.Generic;
using System.Linq;

namespace TddOneOhOne
{
    public class Ocr
    {
        public IEnumerable<int> Recognize(string input)
        {
            var rows = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length != 3)
                throw new MalformedDigitsException();

            var row1Groups = rows.ElementAt(0).Buffer(3);
            var row2Groups = rows.ElementAt(1).Buffer(3);
            var row3Groups = rows.ElementAt(2).Buffer(3);

            return (from sevenSegments in row1Groups.Zip(row2Groups.Zip(row3Groups, Tuple.Create), (a, b) => a.Concat(b.Item1).Concat(b.Item2))
                    select Parse(new string(sevenSegments.ToArray()))).ToList();
        }

        private int Parse(string sevenSegments)
        {
            switch (sevenSegments)
            {
                case " _ | ||_|": return 0;
                case "     |  |": return 1;
                case " _  _||_ ": return 2;
                case " _  _| _|": return 3;
                case "   |_|  |": return 4;
                case " _ |_  _|": return 5;
                case " _ |_ |_|": return 6;
                case " _   |  |": return 7;
                case " _ |_||_|": return 8;
                case " _ |_| _|": return 9;
                default:
                    throw new MalformedDigitsException();
            }
        }
    }
}
