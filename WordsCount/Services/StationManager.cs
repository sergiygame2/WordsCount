﻿using WordsCount.Models;

namespace WordsCount.Services
{
    // Manager for working with logged-in user
    public static class StationManager
    {
        public static User CurrentUser { get; set; }

        public static readonly string UserFilePath;

        static StationManager()
        {
            UserFilePath = "user.json";
        }
    }
}