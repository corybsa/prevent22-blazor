using Prevent22.Shared;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace Prevent22.Client.Services
{
	public interface IUserService
	{
		Task<DbResponse<User>> GetUsers(GridReadEventArgs args);
		Task<DbResponse<Warning>> GetWarnings(int userId);
		Task<DbResponse<User>> UpdateUser(User user);
	}
}
