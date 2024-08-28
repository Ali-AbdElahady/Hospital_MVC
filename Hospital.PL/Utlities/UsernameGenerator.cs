using System.Text.RegularExpressions;

namespace Hospital.PL.Utlities
{
    public static class UsernameGenerator
    {
        private static Random random = new Random();

        public static string GenerateUniqueUsername(string fName, string lName)
        {
            // Combine the first and last name
            string baseUsername = $"{fName}{lName}";

            // Remove any non-alphanumeric characters to keep the username clean
            baseUsername = Regex.Replace(baseUsername, @"[^a-zA-Z0-9]", "");

            // Optionally, make the username lowercase
            baseUsername = baseUsername.ToLower();

            // Append a unique identifier, e.g., a random number
            int uniqueId = random.Next(1000, 9999);  // Generates a random number between 1000 and 9999

            return $"{baseUsername}{uniqueId}";
        }
    }
}
