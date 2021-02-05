using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prevent22.Shared
{
	public class VolunteerRegister : IValidatableObject
	{
		public int EventId { get; set; }
		public int? UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Code { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext context)
		{
			if (!UserId.HasValue)
			{
				// empty first name
				if (string.IsNullOrEmpty(FirstName))
				{
					yield return new ValidationResult(
						errorMessage: "First name is required.",
						memberNames: new[] { "FirstName" }
					);
				}

				// empty email
				if (string.IsNullOrEmpty(Email))
				{
					yield return new ValidationResult(
						errorMessage: "We'll need your email to send you a code in case you need to cancel in the future.",
						memberNames: new[] { "Email" }
					);
				}
				else // invalid email
				{
					var e = new EmailAddressAttribute();

					if (!e.IsValid(Email))
					{
						yield return new ValidationResult(
							errorMessage: "Email is not valid.",
							memberNames: new[] { "Email" }
						);
					}
				}
			}
		}
	}
}
