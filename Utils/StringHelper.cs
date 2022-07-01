using System.Text.RegularExpressions;

public static class StringHelper
{
	public static string RemoveAllSpaces(string str)
	{
		return Regex.Replace(str, @"\s+", "");
	}
}