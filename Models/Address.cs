using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Addresses")]
public class Address
{
	public Address(string city, string addressLine)
	{
		this.City = city;
		this.AddressLine = addressLine;
	}

	public Address()
	{
		this.City = String.Empty;
		this.AddressLine = String.Empty;
	}

	[Column("Id")]
	[Key]
	public long Id { get; set; }

	[Column("City")]
	[Required]
	[MaxLength(50)]
	public string City { get; set; }

	[Column("AddressLine")]
	[Required]
	public string AddressLine { get; set; }
}