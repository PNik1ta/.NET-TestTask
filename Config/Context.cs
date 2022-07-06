using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
/// <summary>
/// This class uses for setting connection to database
/// </summary>
public class Context : DbContext
{

	/// <summary>
	/// Change this connection string to your own local db
	/// </summary>
	public string CONNECTION_STRING = String.Empty;

	public DbSet<Address> Addresses { get; set; }
	public DbSet<Person> People { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(CONNECTION_STRING);
	}
}