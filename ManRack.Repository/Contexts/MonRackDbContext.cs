using ManRack.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManRack.Repository.Contexts
{
	public class MonRackDbContext : DbContext
	{
		public DbSet<Subscriptor> Subscriptors { get; set; }
		public DbSet<EurExchangeRate> EurExchangeRates { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=MonRackdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		}


	}
}
