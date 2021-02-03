using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IAuthService
	{
		Task<AuthResponse<User>> Login(AuthInfo auth);
		Task<DbResponse<User>> Check(int userId);
		Task<AuthResponse<User>> Register(UserRegister user);
	}
}
