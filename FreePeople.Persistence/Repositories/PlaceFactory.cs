using FreePeople.Domain;
using FreePeople.Persistence.DTO;

namespace FreePeople.Persistence.Repositories
{
	public class PlaceFactory : IFactory<Place, PlaceDTO>
	{
		public PlaceDTO CreateDTO(Place e)
		{
			throw new System.NotImplementedException();
		}

		public Place CreateEntity(PlaceDTO dto)
		{
			throw new System.NotImplementedException();
		}
	}
}