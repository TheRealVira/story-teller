using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using story_teller.Config;
using story_teller.Logic;

namespace story_teller.Semantic_Manager
{
    [DataContract]
    public class Sentence:ICountable
    {
        [DataMember]
        public IEnumerable<Word> Words;

        public Sentence(string text, Semantics semantics)
        {
            Words = new List<Word>();
            foreach (var t in text.Split(semantics.WordEnding))
            {
                Words = Words.Add(new Word(t));
            }
        }

        public int Count()
        {
            return Words.Count();
        }
    }
}