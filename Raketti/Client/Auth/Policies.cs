using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raketti.Client.Auth
{
	public static class Policies
	{
		public const string IsGlobalAdmin = "IsGlobalAdmin";
		public const string IsAdmin = "IsAdmin";
		public const string IsTeacher = "IsTeacher";
	}
}
