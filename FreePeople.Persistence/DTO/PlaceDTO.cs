
using System;

namespace FreePeople.Persistence.DTO
{
	internal class PlaceDTO
	{
		public PlaceDTO(Guid id, Guid cityId, string name, string address, string howToGet, string mapUrl, string contactName, string contactPhone)
		{
			Id = id;
			CityId = cityId;
			Name = name;
			Address = address;
			HowToGet = howToGet;
			MapUrl = mapUrl;
			ContactName = contactName;
			ContactPhone = contactPhone;
		}

		public PlaceDTO() { }

		public Guid Id { get; set; }
		public Guid CityId { get; set; }
		public CityDTO City { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string HowToGet { get; set; }
		public string MapUrl { get; set; }
		public string ContactName { get; set; }
		public string ContactPhone { get; set; }
	}
}
