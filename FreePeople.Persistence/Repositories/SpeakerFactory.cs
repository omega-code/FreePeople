using FreePeople.Domain;
using FreePeople.Persistence.DTO;
using Optional;

namespace FreePeople.Persistence.Repositories
{
	public class SpeakerFactory : IFactory<Speaker, SpeakerDTO>
	{
		private readonly CityFactory _cityFactory;

		public SpeakerFactory(CityFactory cityFactory)
		{
			_cityFactory = cityFactory;
		}

		public SpeakerDTO CreateDTO(Speaker e) => 
			new SpeakerDTO(e.Id, e.City.Id, e.Name, e.Photo, e.About, e.Email, e.Facebook.ValueOr((string)null), e.Phone.ValueOr((string)null));

		public Speaker CreateEntity(SpeakerDTO dto) => 
			new Speaker(dto.Id, _cityFactory.CreateEntity(dto.City), dto.Name, dto.Photo, dto.About, dto.Email, dto.Facebook.SomeNotNull(), dto.Phone.SomeNotNull());
	}
}