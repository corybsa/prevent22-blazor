using Raketti.Shared;
using System.Threading.Tasks;

namespace Raketti.Client.Services
{
	public interface IUserService
	{
		Task<DbResponse<User>> GetUsers();
		Task<DbResponse<User>> GetUser(int UserId);
	}
}
