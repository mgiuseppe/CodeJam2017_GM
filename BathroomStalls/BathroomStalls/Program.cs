using System;
using System.Collections.Generic;
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
        }
    }
}
