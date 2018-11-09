using System;
using System.Linq;

namespace story_teller
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sources: https://www.azlyrics.com/m/mychemicalromance.html");

            var documents = IOManager.GetDocumentsFromDirectory("./Input");

            documents.AsParallel().ForAll(x=>x.Safe());

            Console.ReadKey(false);
        }
    }
}
