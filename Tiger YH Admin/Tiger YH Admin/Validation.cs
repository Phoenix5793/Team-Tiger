using System.Text.RegularExpressions;

namespace Tiger_YH_Admin
{
    public static class Validation
    {
        public static bool IsValidEmail(this string email)
        {
            return Regex.IsMatch(email, @"^\S*@([a-z0-9-]+[.])+[a-z]+");
        }

        public static bool IsValidLuhn(this string number)
        {
            int[] deltas = { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            int checksum = 0;
            char[] chars = number.ToCharArray();
            for (int i = chars.Length - 1; i > -1; i--)
            {
                int j = ((int)chars[i]) - 48;
                checksum += j;
                if ((i - chars.Length) % 2 == 0)
                    checksum += deltas[j];
            }
            return (checksum % 10) == 0;
        }
    }
}
