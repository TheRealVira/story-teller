using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using story_teller.IO_Managing;
using story_teller.Semantic_Manager;

namespace story_teller.Logic
{
    [DataContract]
    class WordsFollowing:Word, ISaveable, ICountable
    {
        public const string MyDir = "./Results/";
        public const string FileName = "Relatives";

        [DataMember]
        public ConcurrentDictionary<string, double> Relatives { get; }

        public WordsFollowing(string word) : base(word)
        {
            Relatives = new ConcurrentDictionary<string, double>();
        }

        public WordsFollowing(string word, ConcurrentDictionary<string, double> relatives) : base(word)
        {
            Relatives = relatives;
        }

        public void Save()
        {
            IOManager.Save(this, MyDir + Path.GetFileNameWithoutExtension(FileName) + IOManager.GlobalFileExtension);
        }

        public int Count()
        {
            return Relatives.Count;
        }

        public override bool Equals(object obj)
        {
            return Content.Equals(((WordsFollowing) obj)?.Content);
        }

        public static List<WordsFollowing> Calculate(IEnumerable<Document> documents)
        {
            var weights = new List<WordsFollowing>();
            foreach (var document in documents)
            {
                weights.AddRange(Calculate(document));
            }

            var unionizedWeights = new List<WordsFollowing>();
            foreach (var wordsFollowing in weights)
            {
                if (!unionizedWeights.Contains(wordsFollowing))
                {
                    unionizedWeights.Add(wordsFollowing);
                }

                var currentWeights = unionizedWeights.ElementAt(unionizedWeights.IndexOf(wordsFollowing));

                foreach (var relative in wordsFollowing.Relatives)
                {
                    currentWeights.Relatives.AddOrUpdate(relative.Key, 1, (id, count) => count + 1);
                }
            }

            foreach (var unionizedWeight in unionizedWeights)
            {
                foreach (var unionizedWeightRelative in unionizedWeight.Relatives)
                {
                    unionizedWeight.Relatives.AddOrUpdate(unionizedWeightRelative.Key, 1,
                        (id, count) => (double)(count / documents.Count()));
                }
            }

            return weights;
        }

        public static List<WordsFollowing> Calculate(Document document)
        {
            var toRet = new List<WordsFollowing>();
            for (var i = 0; i < document.Text.Count(); i++)
            {
                var sentences = document.Text.ElementAt(i).Sentences.ToArray();
                for (var j = 0; j < sentences.Count(); j++)
                {
                    var words = sentences.ElementAt(j).Words.ToArray();
                    for (var k = 0; k < words.Count(); k++)
                    {
                        if (k.Equals(words.Count() - 1))
                        {
                            continue;
                        }

                        // If current word already exist inside our dictionary..
                        if (!toRet.Any(x => x.Content.Equals(words.ElementAt(k).Content)))
                        {
                            toRet.Add(new WordsFollowing(words.ElementAt(k).Content));
                        }

                        var index = toRet.Select(x=>x.Content).ToList().IndexOf(words.ElementAt(k).Content);
                        toRet[index].Relatives.AddOrUpdate(words.ElementAt(k+1).Content, 1, (id, count) => count + 1);
                    }
                }
            }

            return toRet;
        }
    }
}
