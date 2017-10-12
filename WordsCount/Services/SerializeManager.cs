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
        // method for creating path to directory where file will be stored
        private static string CreateAndGetPath(string filename)
        {
            try
            {
                if (!Directory.Exists(StaticResources.ClientDirPath))
                {
                    Directory.CreateDirectory(StaticResources.ClientDirPath);
                }
            }
            catch (Exception e)
            {
                Logger.Log($"Error during create {StaticResources.ClientDirPath} directory", e);
            }
            

            return Path.Combine(StaticResources.ClientDirPath, filename);
        }

        public static void Serialize<TObject>(TObject obj) where TObject : ISerializable
        {
            try
            {
                // using json formatter to create .json file with serialized object
                var jsonFormatter = new DataContractJsonSerializer(typeof(TObject));
                string fileName = CreateAndGetPath(obj.FileName);
                
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, obj);
                }
            }
            catch (Exception e)
            {
                // handling exception (writing to log file)
                Logger.Log($"Error during {obj} serialization", e);
            }
        }

        public static TObject Deserialize<TObject>(string fileName) where TObject : ISerializable, new()
        {
            try
            {
                var jsonFormatter = new DataContractJsonSerializer(typeof(TObject));
                var filePath = CreateAndGetPath(fileName);

                if (!File.Exists(filePath))
                {
                    return new TObject();
                }

                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    return (TObject)jsonFormatter.ReadObject(fs);
                }
            }
            catch (Exception e)
            {
                // handling exception (writing to log file)
                Logger.Log($"Error during deserialization. File - {fileName}", e);
                return new TObject();
            }
        }
        
        public static void RemoveFile(string fileName)
        {
            string filePath = CreateAndGetPath(fileName);
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception e)
                {
                    // handling exception (writing to log file)
                    Logger.Log($"Error during {fileName} file remove operation", e);
                }
            }
        }
    }
}
