using ManRack.Repository.Contexts;
using ManRack.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManRack.Repository.Repositories
{
	public class EurExchangeRateRepository : IRepository<EurExchangeRate>
	{
		private readonly MonRackDbContext _context;

		public EurExchangeRateRepository()
		{
			_context = new MonRackDbContext();
		}

		public async Task<IEnumerable<EurExchangeRate>> GetAll()
		{
			return await _context.EurExchangeRates
				.AsQueryable()
				.ToListAsync();
		}

		public async Task<EurExchangeRate> Create(EurExchangeRate entity)
		{
			var valueTask = await _context.EurExchangeRates.AddAsync(entity);
			try
			{

			await valueTask.Context.SaveChangesAsync();
			}
			catch (DbUpdateException e)
			{
				Console.WriteLine(e.Message);
			}
			return entity;
		}
	}
}
