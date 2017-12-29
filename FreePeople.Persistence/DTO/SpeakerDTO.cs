using System;

namespace FreePeople.Persistence.DTO
{
	public class SpeakerDTO
	{
		public SpeakerDTO(Guid id, Guid cityId, string name, byte[] photo, string about, string email, string facebook, string phone)
		{
			Id = id;
			CityId = cityId;
			Name = name;
			Photo = photo;
			About = about;
			Email = email;
			Facebook = facebook;
			Phone = phone;
		}

		public SpeakerDTO() { }

		public Guid Id { get; set; }
		public Guid CityId { get; set; }
		public CityDTO City { get; set; }
		public string Name { get; set; }
		public byte[] Photo { get; set; }
		public string About { get; set; }
		public string Email { get; set; }
		public string Facebook { get; set; }
		public string Phone { get; set; }
	}
}
