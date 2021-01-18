using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raketti.Shared
{
	public class User
	{
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public int RoleId { get; set; }
		public string Email { get; set; }
		public string Title{ get; set; }
		public bool IsActive { get; set; }

		public override string ToString()
		{
			return $"{FirstName} {LastName} ({UserId})";
		}
	}
}
