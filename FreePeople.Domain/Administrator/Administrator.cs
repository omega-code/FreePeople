using System;

namespace FreePeople.Domain
{
	public class Administrator : Entity<Guid>
	{
		public Administrator(Guid id, City city, string name, string email) : base(id)
		{
			City = city;
			Name = name;
			Email = email;
		}

		public City City { get; set; }
		public string Name { get; set;  }
		public string Email { get; }
	}
}
