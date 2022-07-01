using Microsoft.EntityFrameworkCore;

/// <summary>
/// This class uses for setting connection to database
/// </summary>
public class Context : DbContext
{

	/// <summary>
	/// Change this connection string to your own local db
	/// </summary>
	public const string CONNECTION_STRING = @"Server=NIKITA\MSSQLSERVER01;Database=PeopleDB;Trusted_Connection=True;";
	public DbSet<Address> Addresses { get; set; }
	public DbSet<Person> People { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(CONNECTION_STRING);
	}
}