using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Class uses to query persons from database
/// </summary>
public class GetAllRequest
{
	public GetAllRequest(string? firstName, string? lastName, string? city)
	{
		this.FirstName = firstName;
		this.LastName = lastName;
		this.City = city;
	}

	public GetAllRequest()
	{
		this.FirstName = String.Empty;
		this.LastName = String.Empty;
		this.City = String.Empty;
	}

	[FromQuery(Name = "FirstName")]
	public string? FirstName { get; set; }

	[FromQuery(Name = "LastName")]
	public string? LastName { get; set; }

	[FromQuery(Name = "City")]
	public string? City { get; set; }
}