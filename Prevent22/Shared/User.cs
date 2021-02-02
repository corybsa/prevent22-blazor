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
		[Required(ErrorMessage = "System role is required.")]
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		[StringLength(200)]
		public string FirstName { get; set; }
		[StringLength(200)]
		public string LastName { get; set; }
		[EmailAddress]
		[StringLength(255)]
		public string Email { get; set; }
		[StringLength(50)]
		public string Country { get; set; }
		[StringLength(50)]
		public string State { get; set; }
		[StringLength(50)]
		public string City{ get; set; }
		[StringLength(10)]
		public string ZipCode { get; set; }
		[StringLength(50)]
		public string Address { get; set; }
		[Phone]
		[StringLength(20)]
		public string Phone { get; set; }
		public bool IsBanned { get; set; }
		public DateTime? BannedUntil { get; set; }
		public int BannedById { get; set; }
		public string BannedByUsername { get; set; }
	}
}
