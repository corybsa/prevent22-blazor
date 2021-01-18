using Microsoft.AspNetCore.Authorization;
using Raketti.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Raketti.Client.Auth
{
	public class GlobalAdminRequirement : AuthorizationHandler<GlobalAdminRequirement>, IAuthorizationRequirement
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GlobalAdminRequirement requirement)
		{
			try
			{
				var roleId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value);

				if (roleId == SystemRole.GlobalAdmin)
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
