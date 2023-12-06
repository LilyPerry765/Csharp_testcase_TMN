using System.Text;


namespace TMN
{
    public static class StringExtensions
    {

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNumber(this string str)
        {
            foreach (char c in str)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static string BreakCharsToLines(this string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in input)
            {
                sb.AppendLine(c.ToString());
            }
            return sb.ToString().Trim('\n');
        }

    }
}
