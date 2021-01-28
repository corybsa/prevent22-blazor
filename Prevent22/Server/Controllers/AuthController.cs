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
using System.Security.Cryptography;
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
			var response = new AuthResponse<User>();

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

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("Username", auth.Username);

				// get user info from database
				var user = (await _helper.ExecStoredProcedure<User>("sp_Users", parameters)).Data.First();

				if (!CheckHash(user.Hash, auth.Password))
				{
					response.Success = false;
					response.Message = "Username or password is incorrect.";
					return BadRequest(response);
				}

				response.Data = user;
				response.Message = CreateToken(user);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}

			return Ok(response);
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
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register(UserRegister user)
		{
			if (user.Password.CompareTo(user.ConfirmPassword) != 0)
			{
				return BadRequest("Passwords do not match.");
			}

			string hash;
			int iterations = _configuration.GetValue<int>("AppSettings:HashIterations");

			using (var pbkdf2 = new Rfc2898DeriveBytes(user.Password, 32, iterations, HashAlgorithmName.SHA512))
			{
				var key = Convert.ToBase64String(pbkdf2.GetBytes(80));
				var salt = Convert.ToBase64String(pbkdf2.Salt);
				hash = $"{iterations}.{salt}.{key}";
			}

			var response = new AuthResponse<User>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Create);
				parameters.Add("Username", user.Username);
				parameters.Add("Hash", hash);

				var res = (await _helper.ExecStoredProcedure<User>("sp_Users", parameters)).Data.First();

				response.Data = res;
				response.Message = CreateToken(res);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Message = e.Message;
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

		private bool CheckHash(string hash, string password)
		{
			var parts = hash.Split('.', 3);

			if (parts.Length != 3)
			{
				return false;
			}

			var iterations = Convert.ToInt32(parts[0]);
			var salt = Convert.FromBase64String(parts[1]);
			var key = Convert.FromBase64String(parts[2]);

			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512))
			{
				var keyToCheck = pbkdf2.GetBytes(80);
				var verified = keyToCheck.SequenceEqual(key);

				return verified;
			}
		}
	}
}
