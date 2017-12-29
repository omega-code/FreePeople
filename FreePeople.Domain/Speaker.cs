using Optional;
using System;

namespace FreePeople.Domain
{
	public class Speaker : Entity<Guid>
	{
		public Speaker(Guid id, City city, string name, byte[] photo, string about, string email, Option<string> facebook, Option<string> phone) 
			: base(id)
		{
			City = city;
			Name = name;
			Photo = photo;
			About = about;
			Email = email;
			Facebook = facebook;
			Phone = phone;
		}

		public City City { get; set; }
		public string Name { get; set; }
		public byte[] Photo { get; set; }
		public string About { get; set; }
		public string Email { get; }
		public Option<string> Facebook { get; set; }
		public Option<string> Phone { get; set; }
	}
}