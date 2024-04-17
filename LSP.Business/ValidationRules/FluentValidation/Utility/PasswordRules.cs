namespace LSP.Business.ValidationRules.FluentValidation.AuthValidators
{
    public static class PasswordRules
    {
        public static bool ContainsTurkishCharacters(string password)
        {
            return password.ToLower().Contains('ğ') ||
                   password.ToLower().Contains('ü') ||
                   password.ToLower().Contains('ş') ||
                   password.ToLower().Contains('ö') ||
                   password.ToLower().Contains('ç') ||
                   password.ToLower().Contains('ğ') ||
                   password.Contains('İ') ||
                   password.ToLower().Contains('ı');
        }

        public static Func<string, bool> NotStartWith(string prefix)
        {
            return arg => !arg.StartsWith(prefix);
        }

        public static bool NotContainRepeatingCharacters(string password)
        {
            int counter = 1;
            for (int i = 1; i < password.Length; i++)
            {
                if (password[i - 1] == password[i])
                {
                    counter++;
                }
                else
                {
                    counter = 1;
                }

                if (counter > 1) return false;
            }

            return true;
        }

        public static bool NotContainFourSequentialNumbers(string password)
        {
            for (int i = 3; i < password.Length; i++)
            {
                if (password[i] - 1 == password[i - 1] &&
                    password[i - 1] - 1 == password[i - 2] &&
                    password[i - 2] - 1 == password[i - 3])
                {
                    return false;
                }

                if (password[i] + 1 == password[i - 1] &&
                    password[i - 1] + 1 == password[i - 2] &&
                    password[i - 2] + 1 == password[i - 3])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool GreaterThan8(string arg)
        {
            return arg.Length >= 8;
        }
    }
}