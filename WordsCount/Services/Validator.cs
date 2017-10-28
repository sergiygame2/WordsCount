using System.Linq;
using System.Text.RegularExpressions;
using WordsCount.Data;

namespace WordsCount.Services
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

        public static bool IsExistingUsername(string username)
        {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.Users.All(user => user.UserName != username);
            }
        }
    }
}
