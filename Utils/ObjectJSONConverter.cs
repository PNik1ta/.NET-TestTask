using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Custom JSON Serializer for Person model
/// </summary>
public class ObjectJSONConverter
{

	/// <summary>
	/// Convert JSON to instance of a class
	/// </summary>
	public T DeserializeObject<T>(string json)
	{
		T instance = Activator.CreateInstance<T>();
		json = json.Substring(1, json.Length - 2);
		json = StringHelper.RemoveAllSpaces(json);
		string[] keyValues = { };
		List<string> keys = new List<string>();
		List<string> values = new List<string>();
		List<string> nestedObjects = new List<string>();
		while (json.Contains("{"))
		{
			string nestedObj = json.Substring(json.IndexOf("{"), json.IndexOf("}") - json.IndexOf("{") + 1);
			nestedObjects.Add(nestedObj);
			json = json.Remove(json.IndexOf("{"), json.IndexOf("}") - json.IndexOf("{") + 1);
		}
		keyValues = json.Split(',');

		foreach (var keyValue in keyValues)
		{
			keys.Add(keyValue.Split(':')[0].Trim(new Char[] { '\u2019', '\u2018', '\'', '"' }));
			values.Add(keyValue.Split(':')[1].Trim(new Char[] { '\u2019', '\u2018', '\'' }));
		}

		int index = 0;
		for (int i = 0; i < keys.Count; i++)
		{
			foreach (var prop in instance.GetType().GetProperties())
			{
				if (prop.Name.ToLower() == keys[i].ToLower())
				{
					if (prop.PropertyType.IsClass && prop.PropertyType != Type.GetType("System.String"))
					{
						Type type = TypeHelper.GetTypeByName(prop.Name);
						var deserializeMethod = typeof(ObjectJSONConverter).GetMethod("DeserializeObject");
						var methodRef = deserializeMethod.MakeGenericMethod(type);
						prop.SetValue(instance, methodRef.Invoke(new ObjectJSONConverter(), new object[] { nestedObjects[index] }));
						++index;
					}
					else
					{
						prop.SetValue(instance, values[i]);
					}
				}
			}
		}


		return instance;
	}


	/// <summary>
	/// Convert list of objects into JSON
	/// </summary>
	public string SerializeObject<T>(List<T> objects)
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