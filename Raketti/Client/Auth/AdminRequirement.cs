using Microsoft.AspNetCore.Authorization;
using Raketti.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Raketti.Client.Auth
{
	public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
		{
			try
			{
				var roleId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value);

				if (roleId == SystemRole.GlobalAdmin || roleId == SystemRole.Admin)
				{
					context.Succeed(requirement);
				}
			}
			catch (Exception)
			{
				context.Fail();
			}

			return Task.CompletedTask;
		}
	}
}
