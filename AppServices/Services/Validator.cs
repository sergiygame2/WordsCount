using System.Text.RegularExpressions;

namespace AppServices.Services
{
    // Service for validation (currently used for SignUp form, could be used also in other parts of app)
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
    }
}
