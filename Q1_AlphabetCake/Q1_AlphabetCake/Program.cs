using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1_AlphabetCake
{
    class Program
    {
        private static int nLastLine;

        static void Main(string[] args)
        {
            Solve(@"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\Q1_AlphabetCake\Q1_AlphabetCake\A-small-practice.in", @"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\Q1_AlphabetCake\Q1_AlphabetCake\OutputSmall");
            nLastLine = 0;
            Solve(@"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\Q1_AlphabetCake\Q1_AlphabetCake\A-large-practice.in", @"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\Q1_AlphabetCake\Q1_AlphabetCake\OutputLarge");
        }

        private static void Solve(string inputFileName, string outputFileName)
        {
            var lines = File.ReadLines(inputFileName).ToList();
            var nTestCases = int.Parse(lines.ElementAt(nLastLine++));

            var results = new List<string>();
            for (int i = 1; i <= nTestCases; i++)
            {
                results.Add($"Case #{i}:");

                var matrixDims = lines.ElementAt(nLastLine++);
                var nRows = int.Parse(matrixDims.Split(' ')[0]);
                var nCols = int.Parse(matrixDims.Split(' ')[1]);

                var matrix = ReadMatrix(nRows, nCols, lines);
                SolveMatrix(nRows, nCols, matrix);

                results.AddRange(matrix.Select(row => new string(row)));
            }

            File.WriteAllLines(outputFileName, results);
        }

        private static char[][] ReadMatrix(int nRows, int nCols, IEnumerable<string> lines)
        {
            var matrix = new char[nRows][];

            for (int r = 0; r < nRows; r++)
                matrix[r] = lines.ElementAt(nLastLine++).ToArray();

            return matrix;
        }

        private static char[][] SolveMatrix(int nRows, int nCols, char[][] matrix)
        {
            for (int r = 0; r < nRows; r++)
            {
                //fill horizontal
                for (int c = 0; c < nCols; c++)
                {
                    if (matrix[r][c] == '?')
                        continue;

                    //fill left
                    var lc = c - 1;
                    while (lc >= 0 && matrix[r][lc] == '?')
                        matrix[r][lc--] = matrix[r][c];

                    //fill right
                    var rc = c + 1;
                    while (rc < nCols && matrix[r][rc] == '?')
                        matrix[r][rc++] = matrix[r][c];

                    //update next not empty column // -1 to compensate c++
                    c = rc - 1;
                }

                //fill vertical (downside)
                {
                    //fill next row if empty
                    var nextRow = r + 1;
                    while (nextRow < nRows && matrix[nextRow].All(c => c == '?'))
                        matrix[nextRow++] = matrix[r];

                    //update next not empty row // -1 to compensate r++
                    r = nextRow - 1;
                }
            }

            //fill vertical (upside) (last row cannot be empty)
            for (int r = nRows - 2; r >= 0; r--)
            {
                if (matrix[r].All(c => c == '?'))
                    matrix[r] = matrix[r + 1];
            }

            return matrix;
        }
    }
}
