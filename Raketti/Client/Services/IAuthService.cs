using Raketti.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raketti.Client.Services
{
	public interface IAuthService
	{
		Task<AuthResponse<string>> Login(AuthInfo auth);
		Task<DbResponse<User>> Check(int userId);
	}
}
