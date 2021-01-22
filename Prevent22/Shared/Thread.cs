using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prevent22.Shared
{
	public class Thread
	{
		public int? ThreadId { get; set; }
		public int? BoardId { get; set; }
		[Required(ErrorMessage = "A name must be provided.")]
		public string ThreadName { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? LastPostDate { get; set; }
		public string Author { get; set; }
		public string LastPostAuthor { get; set; }
		public int? PostCount { get; set; }
	}
}
