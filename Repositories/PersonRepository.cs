using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
/// <summary>
/// Realize Save and Get operations for Person Model
/// </summary>
public class PersonRepository : IPersonRepository
{
	private readonly Context _dbContext;

	public PersonRepository(IOptions<Context> context)
	{
		this._dbContext = context.Value;
	}


	/// <summary>
	/// Get people by getAllRequest object
	/// </summary>
	/// <param name="getAllRequest"></param>
	/// <returns>JSON string</returns>
	public async Task<string> GetAll(GetAllRequest getAllRequest)
	{
		List<Person> people = new List<Person>();

		if (IsGetAllRequestEmpty.IsAll(getAllRequest))
		{
			System.Console.WriteLine(getAllRequest.LastName);
			System.Console.WriteLine(getAllRequest.LastName == string.Empty);
			people = this._dbContext.People.Where(p =>
				p.Address.City == getAllRequest.City &&
				p.FirstName == getAllRequest.FirstName &&
				p.LastName == getAllRequest.LastName).ToList();
		}

		else if (IsGetAllRequestEmpty.IsCityAndFirstName(getAllRequest))
		{
			people = this._dbContext.People.Where(p =>
				p.Address.City == getAllRequest.City &&
				p.FirstName == getAllRequest.FirstName).ToList();
		}

		else if (IsGetAllRequestEmpty.IsCityAndLastName(getAllRequest))
		{
			people = this._dbContext.People.Where(p =>
				p.Address.City == getAllRequest.City &&
				p.LastName == getAllRequest.LastName).ToList();
		}

		else if (IsGetAllRequestEmpty.IsFirstNameAndLastName(getAllRequest))
		{
			people = this._dbContext.People.Where(p =>
				p.FirstName == getAllRequest.FirstName &&
				p.LastName == getAllRequest.LastName).ToList();
		}

		else if (IsGetAllRequestEmpty.IsCity(getAllRequest))
		{
			people = this._dbContext.People.Where(p => p.Address.City == getAllRequest.City).ToList();
		}

		else if (IsGetAllRequestEmpty.IsFirstName(getAllRequest))
		{
			people = this._dbContext.People.Where(p => p.FirstName == getAllRequest.FirstName).ToList();
		}

		else if (IsGetAllRequestEmpty.IsLastName(getAllRequest))
		{
			people = this._dbContext.People.Where(p => p.LastName == getAllRequest.LastName).ToList();
		}

		else
		{
			people = this._dbContext.People.ToList();
		}


		foreach (var person in people)
		{
			person.Address = await this._dbContext.Addresses.SingleOrDefaultAsync(a => a.Id == person.AddressId);
		}

		string json = PersonJSONConverter.Serialize(people);

		return json;

	}

	/// <summary>
	/// Save new person by json string
	/// </summary>
	/// <param name="json"></param>
	/// <returns>User Id</returns>
	public async Task<long> Save(string json)
	{
		Person person = PersonJSONConverter.DeserializePerson(json);

		if (person == null)
		{
			throw new ArgumentNullException("Person is null");
		}

		await this._dbContext.AddAsync(person);
		await this._dbContext.SaveChangesAsync();
		return person.Id;


	}
}