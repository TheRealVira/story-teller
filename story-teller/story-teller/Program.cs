using System;
using System.Linq;
using story_teller.IO_Managing;
using story_teller.Logic;

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

            Console.WriteLine("Calculating the average of:");
            Console.WriteLine("Paragraph per Story:\t"+documents.AverageOf());
            Console.WriteLine("Sentence per paragraph:\t" + documents.SelectMany(x=>x.Text).AverageOf());
            Console.WriteLine("Words per sentence\t"+documents.SelectMany(x=>x.Text).SelectMany(x=>x.Sentences).AverageOf());

            Console.WriteLine("Calculating relatives...");
            var relatives = WordsFollowing.Calculate(documents);
            var saveableList = new SaveableList<WordsFollowing>(relatives,"results");
            saveableList.Save();
            Console.WriteLine("Finished calculating relatives!");

            Console.WriteLine("Saving documents...");
            documents.AsParallel().ForAll(x=>x.Save());
            Console.WriteLine("Documents saved!");

            Console.ReadKey(false);
        }
    }
}
