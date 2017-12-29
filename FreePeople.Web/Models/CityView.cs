using System;
using FreePeople.Domain;

namespace FreePeople.Web.Models
{
	public class CityView
	{
		public CityView(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public Guid Id { get; }
		public string Name { get; }

		public static CityView FromDomain(City city) => new CityView(city.Id, city.Name);
	}
}
