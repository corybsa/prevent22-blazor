using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Prevent22.Shared
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PasswordAttribute : ValidationAttribute
	{
		public PasswordAttribute() {
			ErrorMessage = "Passwords must contain at least one uppercase letter, one lowercase letter, one number, and one special character (@$!%*?&).";
		}
		
		public override bool IsValid(object? value)
		{
			string password;

			// at least one uppercase letter, one lowercase letter, one number and one special character
			var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$");

			if (value != null)
			{
				password = (string)value;
			}
			else
			{
				return false;
			}

			if (!regex.IsMatch(password))
			{
				return false;
			}

			return true;
		}
	}
}
