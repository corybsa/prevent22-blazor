﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prevent22.Shared
{
	public class Event
	{
		public int EventId { get; set; }
		[Required(ErrorMessage = "Event title is required.")]
		public string Title { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		[Required(ErrorMessage = "Start date is required.")]
		public DateTime Start { get; set; }
		[Required(ErrorMessage = "End date is required.")]
		public DateTime End { get; set; }
		public bool IsAllDay { get; set; }
		public string RecurrenceRule { get; set; }
		public int VolunteerCount { get; set; }
	}
}
