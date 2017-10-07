using System;
using System.IO;
using System.Threading;

namespace WordsCount.Services
{
    static class Logger
    {
        private static readonly string Filepath = Path.Combine(StaticResources.ClientLogDirPath,
            "App" + DateTime.Now.ToString("YYYY_MM_DD") + ".txt");

        private static Mutex mutexObj = new Mutex(true, Filepath);


        private static void CheckingCreateFile()
        {
            if (!Directory.Exists(StaticResources.ClientLogDirPath))
            {
                Directory.CreateDirectory(StaticResources.ClientLogDirPath);
            }
            if (!File.Exists(Filepath))
            {
                File.Create(Filepath).Close();
            }
        }

        internal static void Log(string message)
        {
            mutexObj.WaitOne();
            StreamWriter writer = null;
            FileStream file = null;
            try
            {
                CheckingCreateFile();

                file = new FileStream(Filepath, FileMode.Append);
                writer = new StreamWriter(file);
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss.ms") + " " + message);
            }
            catch
            {
            }
            finally
            {
                writer?.Close();
                file?.Close();
                writer = null;
                file = null;
            }
            mutexObj.ReleaseMutex();

        }

        internal static void Log(string message, Exception ex)
        {


            Log(message);
            var realException = ex;
            while (realException != null)
            {
                Log(realException.Message);
                Log(realException.StackTrace);
                realException = realException.InnerException;
            }

        }

    }
}
