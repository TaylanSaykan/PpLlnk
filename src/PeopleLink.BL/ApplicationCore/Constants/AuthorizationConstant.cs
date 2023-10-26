using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Constants
{
    public static class AuthorizationConstant
    {
        public const string DEFAULT_ADMIN_USER = "sule.cakir@bilgeadam.com";
        public const string DEFAULT_MANAGER_USER = "kaan.kaya@bilgeadamboost.com";
        public const string DEFAULT_DEMO_USER = "ezgi.sonmez@bilgeadamboost.com";
        public const string DEFAULT_PASSWORD = "P@ssword1";

        public static class Roles
        {
            public const string EMPLOYEE = "Employee";
            public const string MANAGER = "Manager";
            public const string ADMIN = "Admin";
        }

        public static class PasswordCreator
        {
            public const string ALL_PASSWORD_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            public const string UPPER_ALPHA_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            public const string LOWER_ALPHA_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            public const string NUMERIC_CHARACTERS = "0123456789";
            public const string SPECIAL_CHARACTERS = "!@#$%^&*";

            public static string CreateDefaultPassword(int passLength = 8)
            {
                StringBuilder builder = new StringBuilder();
                Random rand = new Random();

                for (int i = 0; i < passLength; i++)
                {
                    builder.Append(ALL_PASSWORD_CHARACTERS[rand.Next(ALL_PASSWORD_CHARACTERS.Length)]);
                }

                foreach (char s in builder.ToString())
                {
                    if (UPPER_ALPHA_CHARACTERS.Any(c => builder.ToString().Contains(c)) && LOWER_ALPHA_CHARACTERS.Any(c => builder.ToString().Contains(c)) && NUMERIC_CHARACTERS.Any(c => builder.ToString().Contains(c)) && SPECIAL_CHARACTERS.Any(c => builder.ToString().Contains(c)))
                        return builder.ToString();
                }

               return CreateDefaultPassword(passLength);
            }
        }
    }
}
