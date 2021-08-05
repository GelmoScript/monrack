using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManRack.Repository.Repositories
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> Create(T entity);
	}
}