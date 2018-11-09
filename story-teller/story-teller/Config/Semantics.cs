using System.Runtime.Serialization;
using story_teller.IO_Managing;

namespace story_teller.Config
{
    [DataContract]
    public struct Semantics:ISaveable
    {
        public const string FileName = "Semantics";
        public const string MyDir = "./Config/";

        [DataMember]
        public string ParagraphEnding { get; set; }
        [DataMember]
        public string SentenceEnding { get; set; }
        [DataMember]
        public char WordEnding { get; set; }

        public void Save()
        {
            IOManager.Safe(this, MyDir + FileName + IOManager.GlobalFileExtension);
        }
    }
}
