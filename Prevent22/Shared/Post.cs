using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prevent22.Shared
{
	public class Post
	{
		public int PostId{ get; set; }
		[Required(ErrorMessage = "A message is required to submit a post.")]
		public string Message { get; set; }
		public int CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int? ThreadId { get; set; }
		public string ThreadName { get; set; }
		public string Author { get; set; }
		public int? AuthorRole { get; set; }
		public string RoleName { get; set; }
	}
}
