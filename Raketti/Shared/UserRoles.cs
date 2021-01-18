using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prevent22.Shared
{
	public class UserRoles
	{
		public int RoleId { get; set; }
		public string Rolename { get; set; }
		public int RoleDescription { get; set; }
	}
}
