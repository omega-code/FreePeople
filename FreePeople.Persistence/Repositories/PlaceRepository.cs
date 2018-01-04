using System;
using System.Collections.Generic;
using FreePeople.Domain;
using FreePeople.Persistence.DTO;

namespace FreePeople.Persistence.Repositories
{
	public class PlaceRepository : RepositoryBase<Place, Guid, PlaceDTO, PlaceFactory>, IPlaceRepository
	{
		public PlaceRepository(PlaceFactory factory, string connectionString) : base(nameof(PlaceDTO.Id), factory, connectionString)
		{
		}

		public IReadOnlyCollection<Place> ListForCity(Guid cityId) =>
			Query(x => x.CityId == cityId);
	}
}
