using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prevent22.Shared
{
	public class Board
	{
		public int BoardId { get; set; }
		[Required(ErrorMessage = "A name must be provided.")]
		public string BoardName { get; set; }
		[Required(ErrorMessage = "A description must be provided.")]
		public string BoardDescription { get; set; }
		public int? ThreadCount { get; set; }
		public int? PostCount { get; set; }
		public DateTime? LastPostDate { get; set; }
		public string LastPostAuthor { get; set; }
	}
}
