using System;
using System.Collections.Generic;
using System.Text;

namespace Prevent22.Shared
{
	public class Volunteer
	{
		public int VolunteerId { get; set; }
		public int EventId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; }
		public int RoleId { get; set; }
		public string Email { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public bool IsBanned { get; set; }
		public DateTime BannedUntil { get; set; }
	}
}
