using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using Prevent22.Server.Data;
using Prevent22.Shared;

namespace Prevent22.Server.Controllers
{
	[Auth]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly Helper _helper;
		private readonly IConfiguration _configuration;

		public AuthController(SqlConfiguration sql, IConfiguration configuration)
		{
			_helper = new Helper(sql);
			_configuration = configuration;
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Login(AuthInfo auth)
		{
			var response = new AuthResponse<string>();

			if (auth.Username == string.Empty)
			{
				response.Success = false;
				response.Message = "Username cannot be empty.";
				return BadRequest(response);
			}

			if (auth.Password == string.Empty)
			{
				response.Success = false;
				response.Message = "Password cannot be empty.";
				return BadRequest(response);
			}

			using (var cn = new LdapConnection())
			{
				try
				{
					// connect to ldap
					cn.Connect("ds05", 389);

					// check username and password
					cn.Bind($"LCSD\\{auth.Username}", auth.Password);
					cn.Disconnect();
				}
				catch (LdapException e)
				{
					// username and password combination were wrong
					response.Success = false;
					response.Message = e.Message;
					return BadRequest(response);
				}
				finally
				{
					// close connection to ldap
					cn.Disconnect();
					cn.Dispose();
				}

				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("Username", auth.Username);

				try
				{
					// get user info from database
					var user = (await _helper.ExecStoredProcedure<User>("sp_Users", parameters)).Data.First();
					response.Data = user.UserId.ToString();
					response.Message = CreateToken(user);

					// save user info on client
					Client.Services.UserService.user = user;
				}
				catch (Exception e)
				{
					response.Success = false;
					response.Message = e.Message;
					return StatusCode(StatusCodes.Status500InternalServerError, response);
				}

				return Ok(response);
			}
		}

		[HttpPost("check")]
		public async Task<IActionResult> Check([FromBody] int userId)
		{
			var response = new DbResponse<User>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", 1);
				parameters.Add("UserID", userId);
				var user = (await _helper.ExecStoredProcedure<User>("sp_Users", parameters)).Data;
				response.Success = true;
				response.Data = user;
				Client.Services.UserService.user = user.First();
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}

			return Ok(response);
		}

		private string CreateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, user.RoleId.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}
	}
}
