using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prevent22.Shared
{
	public class Warning : IValidatableObject
	{
		public int WarningId { get; set; }
		[Required(ErrorMessage = "Please provide a reason.")]
		public string WarningReason { get; set; }
		public DateTime WarningDate { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; }
		public int CreatedById { get; set; }
		public string CreatedBy { get; set; }
		public int PostId { get; set; }
		public string PostMessage { get; set; }
		public bool ShouldBanUser { get; set; }
		public DateTime? BanUntil { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext context)
		{
			if(ShouldBanUser) {
				if(BanUntil == null) {
					yield return new ValidationResult(
						errorMessage: "Please choose a date.",
						memberNames: new[] { "BanUntil" }
					);
				}
			}
		}
	}
}
