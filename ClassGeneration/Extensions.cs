namespace ClassGeneration
{
    internal static class Extensions
    {
        internal static string ToCamelCase(this string the_string)
        {
            if (the_string.Length <= 1)
                return the_string.ToLower();

            var first = the_string.ToCharArray(0, 1)[0].ToString();
            return first.ToLower() + new string(the_string.ToCharArray(1, the_string.Length - 1));
        }
    }
}
