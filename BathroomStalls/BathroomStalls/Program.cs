using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathroomStalls
{
    class Program
    {
        static void Main(string[] args)
        {
            //prendo la partizione più grossa (se ce ne sono più di una ne prendo la prima)
            //mi metto al centro della partizione (preferendo la sx se il numero di slot è pari) dividendo di fatto la partizione in due parti
            //lo faccio per tutti i clienti.
            //alla fine prendo la partizione più grossa mi metto al centro (preferendo la sx) e vedo quanto avrei a sx e qnt a dx? questo è il risultato

            //in questo modo evito di fare mille calcoli per ogni cliente (1 macro calcolo per ogni posto libero)

            //dalle 13 alle 13.40 ho pensato al problema e alla soluzione - poi sono andato a mangiare
            //dalle 16.10 ho iniziato a implementare

            Solve(@"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\BathroomStalls\BathroomStalls\C-small-practice-1.in", @"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\BathroomStalls\BathroomStalls\Output-Small-1");
            Solve(@"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\BathroomStalls\BathroomStalls\C-small-practice-2.in", @"C:\Users\Giuseppe\GitHub\CodeJam2017_GM\BathroomStalls\BathroomStalls\Output-Small-2");
        }

        static void Solve(string inputFileName, string outputFileName)
        {
            var lines = File.ReadLines(inputFileName);
            var nTestCases = int.Parse(lines.ElementAt(0));

            var results = new List<string>();
            for (int i = 1; i <= nTestCases; i++)
            {
                results.Add($"Case #{i}: {SolveRow(lines.ElementAt(i))}");
            }

            File.WriteAllLines(outputFileName, results);

        }

        private static string SolveRow(string input)
        {
            var tokens = input.Split(' ');
            var nStalls = int.Parse(tokens[0]);
            var nUsers = int.Parse(tokens[1]);

            SortedList<int, string> partitions = new SortedList<int, string>(Comparer<int>.Create((x,y) => x - y == 0 ? 1 : x - y)) { { nStalls, "partition" } }; //lista ordinata in maniera ascendente (perchè dietro le sorted list ci sono array e non linked list) e fare remove 0 è costoso
            
            int Ls = -1;
            int Rs = -1;

            //n utenti
            for (int i = 0; i < nUsers; i++)
            {
                //seleziono la partizione con più bagni liberi
                var lastIndex = partitions.Count() -1;
                var startPartition = partitions.ElementAt(lastIndex).Key;

                if (startPartition == 0)
                    throw new ApplicationException("è stata scelta una partizione con 0 bagni liberi");

                var chosenStall = (int)Math.Ceiling((decimal)startPartition / 2); //ceiling fatto a mano -> startPartition / 2 + startPartition % 2
                var leftPartition = chosenStall - 1;
                var rightPartition = startPartition - chosenStall;

                Ls = leftPartition;
                Rs = rightPartition;

                partitions.RemoveAt(lastIndex);
                partitions.Add(leftPartition, "leftPartition");
                partitions.Add(rightPartition, "rightPartition");
            }

            return $" {Math.Max(Ls,Rs)} {Math.Min(Ls, Rs)}";
        }
    }
}
