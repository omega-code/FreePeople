using System;
using FreePeople.Domain;
using FreePeople.Persistence.DTO;

namespace FreePeople.Persistence.Repositories
{
	public class TalkFactory : IFactory<Talk, TalkDTO>
	{
		private readonly SpeakerFactory _speakerFactory;
		private readonly CityFactory _cityFactory;
		private readonly AdministratorFactory _administratorFactory;
		private readonly PlaceFactory _placeFactory;

		public TalkFactory(SpeakerFactory speakerFactory, CityFactory cityFactory, AdministratorFactory administratorFactory, PlaceFactory placeFactory)
		{
			_speakerFactory = speakerFactory;
			_cityFactory = cityFactory;
			_administratorFactory = administratorFactory;
			_placeFactory = placeFactory;
		}

		public TalkDTO CreateDTO(Talk e) => new TalkDTO(
			e.Id, e.Name, e.Comment, e.ShortInfo, e.FullInfo, e.StartsAt,
			e.Speaker.Id, e.City.Id, e.Report.ValueOr((string)null), 
			e.ApprovedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null), 
			e.ApprovedBy.Map(x => x.Id).Map(x => (Guid?)x).ValueOr((Guid?)null), 
			e.Place.Map(x => x.Id).Map(x => (Guid?)x).ValueOr((Guid?)null), 
			e.PlaceVerifiedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null), 
			e.PlaceVerifiedBy.Map(x => x.Id).Map(x => (Guid?)x).ValueOr((Guid?)null),
			e.PublishedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null), 
			e.PublishedBy.Map(x => x.Id).Map(x => (Guid?)x).ValueOr((Guid?)null), 
			e.ReportedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null), 
			e.ReportedBy.Map(x => x.Id).Map(x => (Guid?)x).ValueOr((Guid?)null)
		);

		public Talk CreateEntity(TalkDTO dto) => new Talk(
			dto.Id, _cityFactory.CreateEntity(dto.City), dto.StartsAt, _speakerFactory.CreateEntity(dto.Speaker), 
			dto.Name, dto.Comment, dto.ShortInfo, dto.FullInfo
		);
	}
}