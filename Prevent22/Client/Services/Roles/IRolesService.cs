﻿using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IRolesService
	{
		Task<DbResponse<SystemRole>> GetRoles();
	}
}
