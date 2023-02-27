using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Data
{
	public class QuoterContext : DbContext
	{
		private readonly string _connectionString;

		//public DbSet<MigrationHistory> __EFMigrationsHistory { get; set; }

		public DbSet<Collection> Collections { get; set; }

		public DbSet<Book> Books { get; set; }

		public DbSet<Chapter> Chapters { get; set; }

		public DbSet<Quote> Quotes { get; set; }

		//public DbSet<TestEntity> TestEntitys { get; set; }

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
			//modelBuilder.Entity<MigrationHistory>().HasKey(mh => mh.MigrationId);
		}

	
	}
}
