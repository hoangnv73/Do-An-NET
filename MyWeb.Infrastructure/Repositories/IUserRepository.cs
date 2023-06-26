using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Repositories
{
	public interface IUserRepository
	{
		Task<bool> AddAsync(string ma);
		Task<string> GetList(string ma);
	}
}
