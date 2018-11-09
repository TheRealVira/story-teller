using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace story_teller.IO_Managing
{
    [DataContract]
    public class SaveableList<T>:ISaveable
    {
        public const string MyDir = "./Lists/";

        [DataMember]
        private readonly IEnumerable<T> _list;

        private readonly string _name;

        public SaveableList(IEnumerable<T> list, string name)
        {
            _list = list;
            _name = name;
        }

        public void Save()
        {
            IOManager.Save(this, MyDir + Path.GetFileNameWithoutExtension(_name) + IOManager.GlobalFileExtension);
        }
    }
}
