using System;
using System.Collections.Generic;
using System.Text;

namespace Prevent22.Shared
{
	public class SystemRole
	{
		public const int Admin = 1;
		public const int Moderator = 2;
		public const int User = 3;
		public static string[] Names = { "Admin", "Moderator", "User", null };

		public static string GetName(int roleId)
		{
			switch (roleId)
			{
				case Admin:
					return "Admin";
				case Moderator:
					return "Moderator";
				default:
					return "User";
			}
		}

		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public string RoleDescription { get; set; }
	}
}
