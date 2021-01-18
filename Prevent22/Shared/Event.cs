using System;
using System.Collections.Generic;
using System.Text;

namespace Prevent22.Shared
{
	public class Event
	{
		public Event() { }

		public Event(string t, string d, DateTime s, DateTime e, bool i) {
			Title = t;
			Start = s;
			End = e;
			IsAllDay = i;
		}

		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public bool IsAllDay { get; set; }
	}
}
