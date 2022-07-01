/// <summary>
/// This class is using for checking emptiness of getAllRequest properties
/// </summary>

public static class IsGetAllRequestEmpty
{

	public static bool IsCity(GetAllRequest getAllRequest)
	{
		return getAllRequest.City != String.Empty;
	}

	public static bool IsFirstName(GetAllRequest getAllRequest)
	{
		return getAllRequest.FirstName != String.Empty;
	}

	public static bool IsLastName(GetAllRequest getAllRequest)
	{
		return getAllRequest.LastName != String.Empty;
	}

	public static bool IsCityAndFirstName(GetAllRequest getAllRequest)
	{
		return getAllRequest.City != String.Empty &&
		  getAllRequest.FirstName != String.Empty;
	}

	public static bool IsFirstNameAndLastName(GetAllRequest getAllRequest)
	{
		return getAllRequest.FirstName != String.Empty &&
		  getAllRequest.LastName != String.Empty;
	}

	public static bool IsCityAndLastName(GetAllRequest getAllRequest)
	{
		return getAllRequest.City != String.Empty &&
		 getAllRequest.LastName != String.Empty;
	}

	public static bool IsAll(GetAllRequest getAllRequest)
	{
		return getAllRequest.City != String.Empty &&
		  getAllRequest.FirstName != String.Empty &&
		  getAllRequest.LastName != String.Empty;
	}
}