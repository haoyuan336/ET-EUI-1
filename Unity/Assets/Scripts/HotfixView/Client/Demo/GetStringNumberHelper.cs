namespace ET.Client
{
    public static class GetStringNumberHelper
    {
        public static int GetNumber(string str)
        {
            string pattern = @"[^0-9]+";

            string nameString = System.Text.RegularExpressions.Regex.Replace(str, pattern, "");

            int number = int.Parse(nameString);

            return number;
        }

        public static long GetLong(string str)
        {
            string pattern = @"[^0-9]+";

            string nameString = System.Text.RegularExpressions.Regex.Replace(str, pattern, "");

            long number = long.Parse(nameString);

            return number;
        }
    }
}