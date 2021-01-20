﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Auth
{
	public static class Policies
	{
		public const string IsAdmin = "IsAdmin";
		public const string IsModerator = "IsModerator";
		public const string IsUser = "IsUser";
	}
}
