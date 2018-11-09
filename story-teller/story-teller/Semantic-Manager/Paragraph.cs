using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using story_teller.Config;

namespace story_teller.Semantic_Manager
{
    [DataContract]
    internal class Paragraph
    {
        [DataMember]
        private IEnumerable<Sentence> Sentences;

        public Paragraph(string text, Semantics semantics)
        {
            Sentences = new List<Sentence>();
            foreach (var t in text.Split(new [] { semantics.SentenceEnding }, StringSplitOptions.None))
            {
                Sentences = Sentences.Add(new Sentence(t, semantics));
            }
        }
    }
}
