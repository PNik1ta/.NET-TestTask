using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("People")]
public class Person
{
	public Person(string firstName, string lastName, Address address)
	{
		this.FirstName = firstName;
		this.LastName = lastName;
		this.Address = address;
	}
	public Person()
	{
		this.FirstName = String.Empty;
		this.LastName = String.Empty;
		this.Address = new Address(String.Empty, String.Empty);
	}
	
	[Column("Id")]
	[Key]
	public long Id { get; set; }

	[Column("FirstName")]
	[Required]
	public string FirstName { get; set; }

	[Column("LastName")]
	[Required]
	public string LastName { get; set; }

	[Column("AddressId")]
	[ForeignKey("Addresses")]
	public long? AddressId { get; set; }

	[Column("Address")]
	public virtual Address Address { get; set; }
}