using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Custom JSON Serializer for Person model
/// </summary>
public static class PersonJSONConverter
{

	/// <summary>
	/// Convert JSON to person class
	/// </summary>
	public static Person DeserializePerson(string json)
	{
		Person person = new Person();

		List<string> values = new List<string>();

		json = json.Replace("}", String.Empty);
		json = StringHelper.RemoveAllSpaces(json);

		string[] objects = json.Split('{');
		string[] keyValues;

		foreach (var obj in objects)
		{

			keyValues = obj.Split(",");

			foreach (var keyValue in keyValues)
			{
				if (!keyValue.Contains(":") || keyValue.Split(":")[1] == String.Empty)
				{
					continue;
				}

				// This characters are present in test task file.
				string value = keyValue.Split(":")[1].Trim(new Char[] { '\u2019', '\u2018', '\'' });
				values.Add(value);
			}
		}
		string firstName = values[0];
		string lastName = values[1];
		string city = values[2];
		string addressLine = values[3];

		person.FirstName = firstName;
		person.LastName = lastName;
		Address address = new Address(city, addressLine);

		person.Address = address;

		return person;
	}


	/// <summary>
	/// Convert people list into JSON
	/// </summary>
	public static string Serialize(List<Person> people)
	{
		string json = "[";

		foreach (Person person in people)
		{
			json += "{";
			foreach (var prop in person.GetType().GetProperties())
			{
				if (prop.Name == "Address")
				{
					json += string.Format(@"""" + prop.Name + @""": ");
					json += "{";

					Address address = person.Address;
					foreach (var addressProp in address.GetType().GetProperties())
					{
						json += string.Format(@"""" + addressProp.Name + @""": """ + addressProp.GetValue(address) + @""",");
					}

					json += "}";
				}

				else
				{
					json += string.Format(@"""" + prop.Name + @""": """ + prop.GetValue(person) + @""",");
				}

			}
			json += "}";
		}
		json += "]";
		return json;
	}

}