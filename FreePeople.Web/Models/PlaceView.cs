using System;
using FreePeople.Domain;

namespace FreePeople.Web.Models
{
	public class PlaceView
	{
		public PlaceView(Guid id, CityView city, string name, string mapUrl, string howToGet, string address, string contactPhone, string contactName)
		{
			Id = id;
			City = city;
			Name = name;
			MapUrl = mapUrl;
			HowToGet = howToGet;
			Address = address;
			ContactPhone = contactPhone;
			ContactName = contactName;
		}

		public Guid Id { get; }
		public CityView City { get; }
		public string Name { get; }
		public string MapUrl { get; }
		public string HowToGet { get; }
		public string Address { get; }
		public string ContactPhone { get; }
		public string ContactName { get; }

		public static PlaceView FromDomain(Place e) => new PlaceView(e.Id, CityView.FromDomain(e.City), e.Name, e.MapUrl, e.HowToGet, e.Address, e.ContactPhone, e.ContactName);
	}
}