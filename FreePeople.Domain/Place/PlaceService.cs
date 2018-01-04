using System;
using System.Collections.Generic;
using System.Text;

namespace FreePeople.Domain
{
	public class PlaceService
	{
		private readonly IPlaceRepository _placeRepository;

		public PlaceService(IPlaceRepository placeRepository)
		{
			_placeRepository = placeRepository;
		}
		public IReadOnlyCollection<Place> ListFor(City city) => 
			_placeRepository.ListForCity(city.Id);
	}
}
