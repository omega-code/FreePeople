using FreePeople.Domain;
using FreePeople.Persistence.DTO;

namespace FreePeople.Persistence.Repositories
{
	public class CityFactory : IFactory<City, CityDTO>
	{
		public CityDTO CreateDTO(City e) => new CityDTO(e.Id, e.Name);

		public City CreateEntity(CityDTO dto) => new City(dto.Id, dto.Name);
	}
}