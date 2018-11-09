using System;
using System.Linq;

namespace story_teller
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sources: https://www.azlyrics.com/m/mychemicalromance.html");

            Console.WriteLine("Loading documents...");
            var documents = IOManager.GetDocumentsFromDirectory("./Input");
            Console.WriteLine("Documents loaded!");

            Console.WriteLine("Saving documents...");
            documents.AsParallel().ForAll(x=>x.Save());
            Console.WriteLine("Documents saved!");

            Console.ReadKey(false);
        }
    }
}
