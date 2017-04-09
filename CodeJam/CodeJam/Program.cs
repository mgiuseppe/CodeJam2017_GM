using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam
{
    class Program
    {
        static void Main(string[] args)
        {
            Solve(@"C:\Users\Giuseppe\GitHub\MyGitRepository\CodeJam\CodeJam\B-small-practice.in", @"C:\Users\Giuseppe\GitHub\MyGitRepository\CodeJam\CodeJam\OutputSmall");
            Solve(@"C:\Users\Giuseppe\GitHub\MyGitRepository\CodeJam\CodeJam\B-large-practice.in", @"C:\Users\Giuseppe\GitHub\MyGitRepository\CodeJam\CodeJam\OutputLarge");
        }

        static void Solve(string inputFileName, string outputFileName)
        {
            var lines = File.ReadLines(inputFileName);
            var nTestCases = int.Parse(lines.ElementAt(0));

            var results = new List<string>();
            for(int i = 1; i <= nTestCases; i++)
            {
                results.Add($"Case #{i}: {SolveRow(lines.ElementAt(i))}");
            }

            File.WriteAllLines(outputFileName, results);
        }

        private static string SolveRow(string number)
        {
            var lastTidyDigit = 0;
            var nLastTidyDigit = 0;
            var nFirstAppearsLastTidyDigit = 0;//prima occorrenza del lastTidyDigit 

            //trovo il primo digit disordinato
            for(int i = 0; i < number.Length; i++)
            {
                var digit = number[i];

                if (digit >= lastTidyDigit)
                {
                    if (digit > lastTidyDigit)
                        nFirstAppearsLastTidyDigit = i;

                    lastTidyDigit = digit;
                    nLastTidyDigit = i;
                }
                else
                    break; //trovato il primo disordinato
            }

            //se il numero è tidy lo restituisco, altrimenti calcolo il max tidy number minore di number
            return nLastTidyDigit == number.Length -1 ? number : CalculateMaxTidyNumber(number, nFirstAppearsLastTidyDigit);
        }

        /// <summary>
        /// Calcolo:
        /// Ho il numero e la prima occorrenza dell'ultima cifra ordinata.
        /// Divido il numero in due parti: A e B.
        /// A contiene tutte le cifre fino alla prima occorrenza dell'ultima cifra ordinata.
        /// B contiene tutte le cifre che seguono la prima occorrenza dell'ultima cifra ordinata.
        /// 
        /// il max tidy number minore di number sarà: A - 1  seguito da tanti 9 quante sono le cifre in B.
        /// 
        /// ES. 1332 = 999 (A = 13 - B = 32)
        /// ES. 1110 = 999 (A = 1 - B = 110)
        /// </summary>
        /// <returns></returns>
        private static string CalculateMaxTidyNumber(string number, int nFirstAppearsLastTidyDigit)
        {
            var untilFirstAppearsLastTidyDigit = number.Substring(0, nFirstAppearsLastTidyDigit + 1);
            untilFirstAppearsLastTidyDigit = (long.Parse(untilFirstAppearsLastTidyDigit) - 1).ToString();

            var afterFirstAppearsLastTidyDigit = new string('9', number.Length - (nFirstAppearsLastTidyDigit + 1));

            return long.Parse(untilFirstAppearsLastTidyDigit + afterFirstAppearsLastTidyDigit).ToString();

            /*più ordinato
             * var prefix = number.Substring(0, nFirstAppearsLastTidyDigit); //cifre prima della prima occorrenza dell'ultima cifra ordinata
             * var firstAppearsLastTidyDigit = number.Substring(nFirstAppearsLastTidyDigit, 1); //prima occorrenza dell'ultima cifra ordinata
             * var nSuffix = number.Length - (nFirstAppearsLastTidyDigit + 1); //numero di cifre che seguono la prima occorrenza dell'ultima cifra ordinata
             *
             * return long.Parse(prefix + (long.Parse(firstAppearsLastTidyDigit) - 1) + new string('9', nSuffix)).ToString();
             */
        }
    }
}
