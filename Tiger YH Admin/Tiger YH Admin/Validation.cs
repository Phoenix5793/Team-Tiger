using System.Text.RegularExpressions;

namespace Tiger_YH_Admin
{
    public static class Validation
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, $".*@([a-z0-9-]+[.])+[a-z]+");
        }
    }
}
