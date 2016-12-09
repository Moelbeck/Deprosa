using System;

namespace deprosa.Service
{
    public class PasswordValidationService
    {
        public static PasswordValidationService _passwordValidator;

        private PasswordValidationService()
        {
        }

        public static PasswordValidationService GetInstance()
        {
            if (_passwordValidator == null) _passwordValidator = new PasswordValidationService();
            return _passwordValidator;
        }
        public string GenerateCryptedPassword(string userpassword,string salt)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(string.Format(userpassword,salt));
            byte[] computedhash;
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
               computedhash= sha.ComputeHash(data);
            }
            string cryptedpassword = System.Text.Encoding.ASCII.GetString(computedhash);
            return cryptedpassword;
        }
        public string GenerateSalt()
        {
            return Guid.NewGuid().ToString();

        }
        public bool ValidatePassword(string inputPassword, string savedCryptedPassword,string savedSalt)
        {
            if (!string.IsNullOrWhiteSpace(inputPassword) && !string.IsNullOrWhiteSpace(savedCryptedPassword))
            {
                var generatedpass = GenerateCryptedPassword(inputPassword,savedSalt);
                if (string.Compare(generatedpass, savedCryptedPassword, StringComparison.Ordinal) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        

    }
}