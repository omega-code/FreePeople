using FreePeople.Domain;
using FreePeople.Persistence.DTO;

namespace FreePeople.Persistence.Repositories
{
	public class AdministratorFactory : IFactory<Administrator, AdministratorDTO>
	{
		private readonly CityFactory _cityFactory;

		public AdministratorFactory(CityFactory cityFactory)
		{
			_cityFactory = cityFactory;
		}

		public AdministratorDTO CreateDTO(Administrator e) => new AdministratorDTO(e.Id, e.City.Id, e.Name, e.Email);

		public Administrator CreateEntity(AdministratorDTO dto) => new Administrator(dto.Id, _cityFactory.CreateEntity(dto.City), dto.Name, dto.Email);
	}
}