using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prevent22.Shared
{
	public class User
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Hash { get; set; }
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string City{ get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public bool IsBanned { get; set; }
		public DateTime BannedUntil { get; set; }
		public int BannedById { get; set; }
		public string BannedByUsername { get; set; }
	}
}
