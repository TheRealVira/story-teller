using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using story_teller.Config;
using story_teller.IO_Managing;

namespace story_teller
{
    internal class IOManager
    {
        public const string GlobalFileExtension = ".json";

        public static Semantics GlobalSemantics = new Semantics()
        {
            ParagraphEnding = Environment.NewLine+Environment.NewLine,
            SentenceEnding = Environment.NewLine,
            WordEnding = ' '
        };

        public static IEnumerable<Document> GetDocumentsFromDirectory(string path)
        {
            if (!Directory.Exists(path)) yield break;

            foreach (var s in Directory.GetFiles(path).AsParallel())
            {
                yield return new Document(s, GlobalSemantics);
            }
        }

        public static async void Save<T>(T data, string path) where T : ISaveable
        {
            EnsureDirectoryExists(path);
            using (var writer = new StreamWriter(path, false))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }

        public static async void Save<T>(IEnumerable<T> data, string path) where T : ISaveable
        {
            EnsureDirectoryExists(path);
            using (var writer = new StreamWriter(path, false))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }

        public static async Task<T> Load<T>(string path) where T : ISaveable
        {
            using (var reader = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
            }
        }

        private static void EnsureDirectoryExists(string filePath)
        {
            var fi = new FileInfo(filePath);
            if (fi.Directory != null && !fi.Directory.Exists)
            {
                Directory.CreateDirectory(fi.DirectoryName);
            }
        }
    }
}
