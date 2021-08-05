using ManRack.Repository.Contexts;
using ManRack.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManRack.Repository.Repositories
{
	public class SubscriptorRepository : IRepository<Subscriptor>
	{
		private readonly MonRackDbContext _context;

		public SubscriptorRepository(MonRackDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Subscriptor>> GetAll()
		{
			return await _context.Subscriptors
				.AsQueryable()
				.ToListAsync();
		}

		public async Task<Subscriptor> Create(Subscriptor entity)
		{
			var valueTask = await _context.Subscriptors.AddAsync(entity);
			await valueTask.Context.SaveChangesAsync();
			return entity;
		}
	}
}
