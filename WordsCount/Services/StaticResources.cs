﻿using System;
using System.IO;

namespace WordsCount.Services
{
    static class StaticResources
    {
        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        internal static readonly string ClientDirPath = Path.Combine(AppData, "WordsCount");
        internal static readonly string ClientLogDirPath = Path.Combine(ClientDirPath, "Logs");
    }
}
