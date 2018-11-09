using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using story_teller.Config;
using story_teller.Logic;

namespace story_teller.Semantic_Manager
{
    [DataContract]
    internal class Paragraph:ICountable
    {
        [DataMember]
        public IEnumerable<Sentence> Sentences;

        public Paragraph(string text, Semantics semantics)
        {
            Sentences = new List<Sentence>();
            foreach (var t in text.Split(new [] { semantics.SentenceEnding }, StringSplitOptions.None))
            {
                Sentences = Sentences.Add(new Sentence(t, semantics));
            }
        }

        public int Count()
        {
            return Sentences.Count();
        }
    }
}
