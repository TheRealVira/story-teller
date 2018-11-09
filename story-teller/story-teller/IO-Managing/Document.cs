using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using story_teller.Config;
using story_teller.IO_Managing;
using story_teller.Semantic_Manager;

namespace story_teller
{
    [DataContract]
    internal class Document:ISaveable
    {
        private const string MyDir = "./Documents/";

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public IEnumerable<Paragraph> Text { get; set; }
        [DataMember]
        public readonly string Filename;

        public Document(string path, Semantics semantics)
        {
            Filename = Path.GetFileName(path);
            using (var reader = new StreamReader(path))
            {
                Title = reader.ReadLine();
                Text = new List<Paragraph>();

                foreach (var paragraph in reader.ReadToEnd().Split(new []{semantics.ParagraphEnding}, StringSplitOptions.None))
                {
                    Text = Text.Add(new Paragraph(paragraph, semantics));
                }
            }
        }

        public Document(string title, string text)
        {
            Title = title;

        }

        public void Save()
        {
            IOManager.Safe(this, MyDir + Path.GetFileNameWithoutExtension(Filename) + IOManager.GlobalFileExtension);
        }
    }
}
