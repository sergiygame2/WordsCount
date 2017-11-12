using System;
using System.IO;
using System.Text;
using System.Threading;

namespace AppServices.Services
{
    public static class Logger
    {
        private static readonly string Filepath;
        private static readonly Mutex MutexObj;

        // initializing log path & check if it is valid (directory and file is created)
        static Logger()
        {
            Filepath = Path.Combine(StaticResources.ClientLogDirPath, DateTime.Now.ToString("yyyy_MM_dd") + ".txt");
            CheckingCreateFile();
            MutexObj = new Mutex(true, Filepath.Replace(Path.DirectorySeparatorChar, '_'));
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

        public static void Log(string message)
        {
            // use mutex to lock file (inner processes mutex)
            MutexObj.WaitOne();
            StreamWriter writer = null;
            FileStream file = null;
            try
            {
                // initialize writer and get access to file
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
            // relsease mutex to unlock file
            MutexObj.ReleaseMutex();
        }

        // method for writing exceptions to log file
        public static void Log(string message, Exception ex)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(message);
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }

        public static void Log(Exception ex)
        {
            var stringBuilder = new StringBuilder();
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }
    }
}
