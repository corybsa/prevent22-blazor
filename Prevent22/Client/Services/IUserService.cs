using Prevent22.Shared;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IUserService
	{
		Task<DbResponse<User>> GetUsers();
		Task<DbResponse<User>> GetUser(int UserId);
	}
}
