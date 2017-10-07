using System;
using System.IO;
using System.Threading;

namespace WordsCount.Services
{
    internal static class Logger
    {
        private static readonly string Filepath;
        private static readonly Mutex MutexObj;

        static Logger()
        {
            Filepath = Path.Combine(StaticResources.ClientLogDirPath, DateTime.Now.ToString("yyyy_MM_dd") + ".txt");
            CheckingCreateFile();
            //MutexObj = new Mutex(true, Filepath);
        }

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
            //MutexObj.WaitOne();
            StreamWriter writer = null;
            FileStream file = null;
            try
            {
                file = new FileStream(Filepath, FileMode.Append);
                writer = new StreamWriter(file);
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss.ms") + " " + message);
            }
            finally
            {
                writer?.Close();
                file?.Close();
                writer = null;
                file = null;
            }
            //MutexObj.ReleaseMutex();
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
