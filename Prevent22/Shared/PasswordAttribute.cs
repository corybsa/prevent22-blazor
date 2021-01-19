using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Prevent22.Shared
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class PasswordAttribute : ValidationAttribute
	{
		private string Pattern;

		public PasswordAttribute(string pattern) {
			Pattern = pattern;
		}
		
		public override bool IsValid(object? value)
		{
			string password;

			// at least one lowercase letter, one uppercase letter, one number and one special character
			var regex = new Regex(Pattern);

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
