using System;
using System.Text;

namespace AppServices.Helpers
{
    public static class DataHelper
    {
        // method for hashing text (currently used for hashing password)
        public static string Hash(string text)
        {
            var bytes = new UTF8Encoding().GetBytes(text);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
