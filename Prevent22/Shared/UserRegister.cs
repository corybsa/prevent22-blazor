﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prevent22.Shared
{
	public class UserRegister
	{
		[Required, StringLength(255, ErrorMessage = "Username is too long (255 characters max)")]
		public string Username { get; set; }
		[Required, Password, StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
