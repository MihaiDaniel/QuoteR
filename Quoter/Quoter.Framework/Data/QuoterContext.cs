using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Entities;

namespace Quoter.Framework.Data
{
	public class QuoterContext : DbContext
	{
		private readonly string _connectionString;

		public DbSet<Collection> Collections { get; set; }

		public DbSet<Book> Books { get; set; }

		public DbSet<Chapter> Chapters { get; set; }

		public DbSet<Quote> Quotes { get; set; }

		public DbSet<Log> Logs { get; set; }

		/// <summary>
		/// Default constructor needed for ef tools migrations to intialize the context
		/// </summary>
		public QuoterContext()
		{

		}

		public QuoterContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
		}

	
	}
}
