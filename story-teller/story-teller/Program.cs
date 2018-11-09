using System;
using System.Linq;
using story_teller.IO_Managing;
using story_teller.Logic;

namespace story_teller
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Sources: https://www.azlyrics.com/m/mychemicalromance.html");

            Console.WriteLine("Loading documents...");
            var documents = IOManager.GetDocumentsFromDirectory("./Input");
            Console.WriteLine("Documents loaded!");

            Console.WriteLine("Calculating the average of:");
            var documentArray = documents as Document[] ?? documents.ToArray();
            Console.WriteLine("Paragraph per Story:\t"+documentArray.AverageOf());
            Console.WriteLine("Sentence per paragraph:\t" + documentArray.SelectMany(x=>x.Text).AverageOf());
            Console.WriteLine("Words per sentence\t"+documentArray.SelectMany(x=>x.Text).SelectMany(x=>x.Sentences).AverageOf());

            Console.WriteLine("Calculating relatives...");
            var relatives = WordsFollowing.Calculate(documentArray);
            IOManager.Save(relatives, "./Results/relatives.json");
            //saveableList.Save();
            Console.WriteLine("Finished calculating relatives!");

            Console.WriteLine("Saving documents...");
            documentArray.AsParallel().ForAll(x=>x.Save());
            Console.WriteLine("Documents saved!");

            Console.ReadKey(true);
        }
    }
}
