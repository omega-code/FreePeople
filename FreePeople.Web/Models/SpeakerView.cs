using System;
using FreePeople.Domain;
using Optional;

namespace FreePeople.Web.Models
{
	public class SpeakerView
	{
		public static SpeakerView FromDomain(Speaker e) => new SpeakerView(e.Id, CityView.FromDomain(e.City), e.Name, e.Photo, e.About, e.Email, e.Facebook, e.Phone);

		public SpeakerView(Guid id, CityView city, string name, byte[] photo, string about, string email, Option<string> facebook, Option<string> phone)
		{
			Id = id;
			City = city;
			Name = name;
			Photo = photo;
			About = about;
			Email = email;
			Facebook = facebook;
			Phone = phone;
		}

		public Guid Id { get; }
		public CityView City { get; }
		public string Name { get; }
		public byte[] Photo { get; }
		public string About { get; }
		public string Email { get; }
		public Option<string> Facebook { get; }
		public Option<string> Phone { get; }
	}
}
