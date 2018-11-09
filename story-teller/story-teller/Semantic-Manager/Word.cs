using System.Runtime.Serialization;

namespace story_teller.Semantic_Manager
{
    [DataContract]
    internal class Word
    {
        [DataMember]
        private string Content;

        public Word(string content)
        {
            this.Content = content;
        }
    }
}