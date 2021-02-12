﻿using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

		public VolunteersController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CreateVolunteer(VolunteerRegister volunteer)
		{
			DbResponse<Event> response;

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

				// TODO: Email code to person
			}
			catch (ApiException<Event> e)
			{
				response = e.Response;
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

			return new string(array).Substring(0, 10);
		}
	}
}
