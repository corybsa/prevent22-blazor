using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Prevent22.Server.Data;
using Prevent22.Shared;

namespace Prevent22.Server.Controllers
{
	[Auth]
	[Route("api/[controller]")]
	[ApiController]
	public class VolunteersController : ControllerBase
	{
		private readonly Helper _helper;
		private readonly IConfiguration _config;

		public VolunteersController(SqlConfiguration sql, IConfiguration config)
		{
			_helper = new Helper(sql);
			_config = config;
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CreateVolunteer(VolunteerRegister volunteer)
		{
			var response = new DbResponse<Event>();

			try
			{
				string code = GenerateCode();
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Create);
				parameters.Add("EventId", volunteer.EventId);
				parameters.Add("UserId", volunteer.UserId);
				parameters.Add("FirstName", volunteer.FirstName);
				parameters.Add("LastName", volunteer.LastName);
				parameters.Add("Email", volunteer.Email);
				parameters.Add("Code", code);
				response = await _helper.ExecStoredProcedure<Event>("sp_Volunteers", parameters);

				Event e = response.Data.First();
				string server = _config.GetValue<string>("Email:Server");
				string username = _config.GetValue<string>("Email:Username");
				string password = _config.GetValue<string>("Email:Password");
				string toName = "Cory Sandlin";
				string toEmail = "corybsa@gmail.com";
				string subject = $"Registration for {e.Title}";
				string body = System.IO.File.ReadAllText("Data/html/emails/volunteer-register.html");
				body = body.Replace("##EVENT_TITLE##", e.Title)
						   .Replace("##EVENT_START##", e.Start.ToString("MMM dd yyyy hh:mm tt"))
						   .Replace("##EVENT_END##", e.End.ToString("MMM dd yyyy hh:mm tt"))
						   .Replace("##EVENT_LOCATION##", e.Location ?? "No location specified")
						   .Replace("##REGISTRATION_CODE##", code);

				_helper.SendEmail(toName, toEmail, subject, body, server, username, password);
			}
			catch (ApiException<Event> e)
			{
				response = e.Response;
				return BadRequest(response);
			}
			catch (Exception e)
			{
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpDelete("{volunteerId}")]
		public async Task<IActionResult> DeleteVolunteer(int volunteerId)
		{
			DbResponse<Volunteer> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Delete);
				parameters.Add("VolunteerId", volunteerId);
				response = await _helper.ExecStoredProcedure<Volunteer>("sp_Volunteers", parameters);
			}
			catch (ApiException<Volunteer> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpDelete("{eventId}/{code}")]
		public async Task<IActionResult> DeleteVolunteerByCode(int eventId, string code)
		{
			DbResponse<Volunteer> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Delete);
				parameters.Add("EventId", eventId);
				parameters.Add("Code", code);
				response = await _helper.ExecStoredProcedure<Volunteer>("sp_Volunteers", parameters);
			}
			catch (ApiException<Volunteer> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		private string GenerateCode()
		{
			string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			char[] array = str.ToCharArray();
			Random rng = new Random();
			int n = array.Length;

			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				var value = array[k];
				array[k] = array[n];
				array[n] = value;
			}

			return new string(array).Substring(0, 6);
		}
	}
}
