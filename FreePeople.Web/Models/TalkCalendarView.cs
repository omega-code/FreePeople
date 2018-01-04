using System;
using System.Collections.Generic;

namespace FreePeople.Web.Models
{
	public class TalkCalendarView
    {
		public TalkCalendarView(DateTime month, CityView city, IReadOnlyCollection<TalkView> talks)
		{ 
			Month = month;
			City = city;
			Talks = talks;
		}

		public DateTime Month { get; }
		public CityView City { get; }
		public IReadOnlyCollection<TalkView> Talks { get; }
	}
}
