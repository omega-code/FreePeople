using System;

namespace FreePeople.Domain
{
	public class City : Entity<Guid>
	{
		public City(Guid id, string name) : base(id)
		{
			Name = name;
		}

		public string Name { get; set;  }
	}
}