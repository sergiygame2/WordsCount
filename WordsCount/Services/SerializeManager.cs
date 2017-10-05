using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WordsCount.Services
{
    internal interface ISerializable
    {
        string FileName { get; }
    }

    internal static class SerializeManager
    {
        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string DirPath = Path.Combine(AppData, "WordsCount");

        private static string CreateAndGetPath(string filename)
        {
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);

            return Path.Combine(DirPath, filename);
        }

        public static void Serialize<TObject>(TObject obj) where TObject : ISerializable
        {
            try
            {
                var jsonFormatter = new DataContractJsonSerializer(typeof(TObject));
                var fileName = CreateAndGetPath(obj.FileName);
                
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, obj);
                }
            }
            catch (Exception e)
            {
                // TODO Add logging
                throw;
            }
        }

        public static TObject Deserialize<TObject>(string fileName) where TObject : ISerializable, new()
        {
            try
            {
                var jsonFormatter = new DataContractJsonSerializer(typeof(TObject));
                var filePath = CreateAndGetPath(fileName);
                if (!File.Exists(filePath))
                    throw new FileNotFoundException();

                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    return (TObject)jsonFormatter.ReadObject(fs);
                }
            }
            catch (Exception e)
            {
                // TODO add logging
                return new TObject();
            }
        }
    }
}
