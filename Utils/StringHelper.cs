using System.Text.RegularExpressions;

/// <summary>
/// This class helps work with string
/// </summary>
public static class StringHelper
{
	public static string RemoveAllSpaces(string str)
	{
		return Regex.Replace(str, @"\s+", "");
	}

	public static string FindStringBetweenTwoStrings(string str, string from, string to)
	{
		int pFrom = str.IndexOf(from) + from.Length;
		int pTo = str.LastIndexOf(to);
		return str.Substring(pFrom, pTo - pFrom);
	}
}