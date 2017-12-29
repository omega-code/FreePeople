using System;

namespace FreePeople.Domain
{
	public class Place : Entity<Guid>
	{
		public Place(Guid id, City city, string name, string address, string howToGet, string mapUrl, string contactName, string contactPhone) : base(id)
		{
			City = city;
			Name = name;
			Address = address;
			HowToGet = howToGet;
			MapUrl = mapUrl;
			ContactName = contactName;
			ContactPhone = contactPhone;
		}

		public City City { get; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string HowToGet { get; set; }
		public string MapUrl { get; set; }
		public string ContactName { get; set; }
		public string ContactPhone { get; set; }
	}
}