using System;
using System.Collections.Generic;
using System.Text;

namespace Prevent22.Shared
{
	public class ForumPost
	{
		public ForumPost(string t, string a)
		{
			Title = t;
			Author = a;
		}

		public string Title { get; set; }
		public string Author { get; set; }
	}
}
