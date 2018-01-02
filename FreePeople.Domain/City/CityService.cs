using System;
using System.Collections.Generic;
using System.Text;

namespace FreePeople.Domain
{
    public class CityService
    {
		private readonly ICityRepository _cityRepository;

		public CityService(ICityRepository cityRepository)
		{
			_cityRepository = cityRepository;
		}

		public IReadOnlyCollection<City> ListAll() => _cityRepository.List();
	}
}
