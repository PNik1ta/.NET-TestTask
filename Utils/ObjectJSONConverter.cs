using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Custom JSON Serializer for Person model
/// </summary>
public static class ObjectJSONConverter<T> where T : class
{

	/// <summary>
	/// Convert JSON to instance of a class
	/// </summary>
	public static T DeserializeObject(string json)
	{
		T instance = Activator.CreateInstance<T>();

		List<string> values = new List<string>();
		List<string> keys = new List<string>();

		json = json.Replace("}", String.Empty);
		json = StringHelper.RemoveAllSpaces(json);

		string[] objects = json.Split('{');
		string[] keyValues = { };
		string lastKey = String.Empty;

		foreach (string obj in objects)
		{

			keyValues = obj.Split(",");


			if (obj != objects[1] && obj != objects[0])
			{
				object nestedInstance = new object();
				string className = char.ToUpper(lastKey[0]) + lastKey.Substring(1);
				Type type = Type.GetType(className);
				if (type != null)
				{
					nestedInstance = Activator.CreateInstance(type);
					int index = 0;
					foreach (var prop in nestedInstance.GetType().GetProperties())
					{
						if (prop.Name == "Id")
						{
							continue;
						}
						prop.SetValue(nestedInstance, keyValues[index].Split(":")[1].Trim(new Char[] { '\u2019', '\u2018', '\'' }));
						++index;
					}

					// Assign nested object to last property
					instance.GetType().GetProperties()[instance.GetType().GetProperties().Count() - 1].SetValue(instance, nestedInstance);
				}
			}

			//Get key before nested object
			lastKey = keyValues[keyValues.Length - 1].Split(':')[0].Trim(new Char[] { '\u2019', '\u2018', '\'', '"' });

			foreach (var keyValue in keyValues)
			{
				if (!keyValue.Contains(":") || keyValue.Split(":")[1] == String.Empty)
				{
					continue;
				}

				// This characters are present in test task file.
				string value = keyValue.Split(":")[1].Trim(new Char[] { '\u2019', '\u2018', '\'' });
				string key = keyValue.Split(':')[0].Trim(new Char[] { '\u2019', '\u2018', '\'', '"' });
				keys.Add(key);
				values.Add(value);
			}
		}

		for (int i = 0; i < keys.Count; i++)
		{
			foreach (var prop in instance.GetType().GetProperties())
			{
				if (prop.Name.ToLower() == keys[i].ToLower())
				{
					prop.SetValue(instance, values[i]);
				}
			}
		}
		return instance;
	}


	/// <summary>
	/// Convert list of objects into JSON
	/// </summary>
	public static string SerializeObject(List<T> objects)
	{
		string json = "[";

		foreach (T obj in objects)
		{
			json += "{";
			foreach (var prop in obj.GetType().GetProperties())
			{
				if (prop.PropertyType.IsClass && prop.PropertyType != Type.GetType("System.String"))
				{
					json += string.Format(@"""" + prop.Name + @""": ");
					json += "{";

					object nestedInstance = Activator.CreateInstance(prop.PropertyType);
					if (nestedInstance != null)
					{
						int index = 0;
						foreach (var nestedProp in nestedInstance.GetType().GetProperties())
						{
							var nestedObj = obj.GetType().GetProperty(prop.Name).GetValue(obj);
							nestedProp.SetValue(nestedInstance, nestedObj.GetType().GetProperties()[index].GetValue(nestedObj));
							++index;
						}
					}

					foreach (var nestedProp in nestedInstance.GetType().GetProperties())
					{
						json += string.Format(@"""" + nestedProp.Name + @""": """ + nestedProp.GetValue(nestedInstance) + @""",");
					}

					json += "}";
				}

				else
				{
					json += string.Format(@"""" + prop.Name + @""": """ + prop.GetValue(obj) + @""",");
				}

			}
			json += "}";
		}
		json += "]";
		return json;
	}

}