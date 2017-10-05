using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordsCount.Data;

namespace WordsCount.Services
{
    public class Validator
    {
        public static bool IsString(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public static bool IsPasswordMatch(string password, string repeatedPassword)
        {
            return repeatedPassword == password;
        }

        public static bool IsExistingUsername(string username)
        {
            return !DbAdapter.Users.Any(user => user.UserName == username);
        }
    }
}
