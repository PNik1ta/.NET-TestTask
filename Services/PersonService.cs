/// <summary>
/// This class implements the work of find and insert People
/// </summary>

public class PersonService : IPersonService
{
	private readonly IPersonRepository personRepository;

	public PersonService(IPersonRepository personRepository)
	{
		this.personRepository = personRepository;
	}
	public async Task<string> GetAll(GetAllRequest getAllRequest)
	{
		return await personRepository.GetAll(getAllRequest);
	}

	public async Task<long> Save(string json)
	{
		return await personRepository.Save(json);
	}
}