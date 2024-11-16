using System.Security.Cryptography;
using System.Text;

namespace WeddingPlanner.Helpers
{
    public class PasswhordHash
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert hash byte array to hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Hash the entered password and compare it with the stored hash
            string enteredHash = HashPassword(enteredPassword);
            return enteredHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
