using System;
using FreePeople.Domain;
using FreePeople.Persistence.DTO;

namespace FreePeople.Persistence.Repositories
{
	public class CityRepository : RepositoryBase<City, Guid, CityDTO, CityFactory>, ICityRepository
	{
		public CityRepository(CityFactory factory, string connectionString) : base(nameof(CityDTO.Id), factory, connectionString)
		{
		}
	}
}
