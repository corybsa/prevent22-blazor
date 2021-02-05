﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Prevent22.Shared;
using System;

namespace Prevent22.Server.Data
{
	public class AuthAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		public new int[] Roles { get; set; }

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			try
			{
				// No roles defined, don't do role checking
				if (Roles == null)
				{
					return;
				}

				// Empty array of roles, reject all roles
				if (Roles.Length == 0)
				{
					context.Result = new UnauthorizedResult();
					return;
				}

				var user = Client.Services.UserService.user;
				Console.WriteLine(user.UserId);

				// wait for user data
				while (user == null)
				{
					user = Client.Services.UserService.user;
				}

				// Global admin can access everything
				if (user.RoleId == SystemRole.Admin)
				{
					return;
				}

				// Check user role against roles passed in
				foreach (int role in Roles)
				{
					if (user.RoleId == role)
					{
						// User has an acceptable role
						return;
					}
				}

				// User didn't have acceptable role
				context.Result = new UnauthorizedResult();
				return;
			}
			catch {
				Console.WriteLine("exception");
				context.Result = new UnauthorizedResult();
				return;
			}
		}
	}
}
