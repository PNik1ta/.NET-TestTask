using Microsoft.AspNetCore.Mvc;
/// <summary>
/// The controller which save and returns people from db
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PersonController : ControllerBase
{
	private readonly IPersonService personService;

	public PersonController(IPersonService personService)
	{
		this.personService = personService;
	}

	[HttpGet("getAll")]
	public async Task<ActionResult<string>> GetAll([FromQuery] GetAllRequest getAllRequest)
	{
		try
		{
			string json = await personService.GetAll(getAllRequest);
			return base.Ok(json);
		}
		catch (Exception ex)
		{
			return base.BadRequest(ex);
		}

	}

	[HttpPost("save")]
	public async Task<ActionResult<long>> Save([FromBody] string json)
	{
		try
		{
			long id = await personService.Save(json);
			return base.Ok(id);
		}
		catch (Exception ex)
		{
			return base.BadRequest(ex);
		}

	}
}