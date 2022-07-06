public static class TypeHelper
{
	public static Type GetTypeByName(string name)
	{
		return
			 AppDomain.CurrentDomain.GetAssemblies()
				  .Reverse()
				  .Select(assembly => assembly.GetType(name))
				  .FirstOrDefault(t => t != null)
			 // Safely delete the following part
			 // if you do not want fall back to first partial result
			 ??
			 AppDomain.CurrentDomain.GetAssemblies()
				  .Reverse()
				  .SelectMany(assembly => assembly.GetTypes())
				  .FirstOrDefault(t => t.Name.Contains(name));
	}
}