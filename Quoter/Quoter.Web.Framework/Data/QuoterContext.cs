using Microsoft.EntityFrameworkCore;
using Quoter.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Web.Framework.Data
{
	public class QuoterContext : DbContext
	{
		public QuoterContext()
		{

		}

		public QuoterContext(DbContextOptions<QuoterContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<QuoterUpdate>(entity =>
			//{
			//	entity.HasKey(quoterUpdate => quoterUpdate.Id);
			//});
		}

		public DbSet<QuoterUpdate> QuoterUpdates { get; set; }

		public DbSet<User> Users { get; set; }
	}
}
