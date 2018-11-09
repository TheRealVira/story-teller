using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using story_teller.Config;

namespace story_teller.Semantic_Manager
{
    [DataContract]
    public class Sentence
    {
        [DataMember]
        public IEnumerable<Word> Words;

        public int WordCount => Words.Count();

        public Sentence(string text, Semantics semantics)
        {
            Words = new List<Word>();
            foreach (var t in text.Split(semantics.WordEnding))
            {
                Words = Words.Add(new Word(t));
            }
        }
    }
}