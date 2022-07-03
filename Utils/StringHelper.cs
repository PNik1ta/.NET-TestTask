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
}