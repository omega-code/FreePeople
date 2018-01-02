using System;
using System.Linq;
using FreePeople.Domain;
using FreePeople.Persistence.DTO;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace FreePeople.Persistence.Repositories
{
	public class TalkRepository : RepositoryBase<Talk, Guid, TalkDTO, TalkFactory>, ITalkRepository
	{
		public TalkRepository(TalkFactory factory, string connectionString) : base(nameof(SpeakerDTO.Id), factory, connectionString)
		{
		}

		public Option<Talk> FindTalkFor(Guid speakerId, TalkStatus status) =>
			Query(t => t.SpeakerId == speakerId)
				.ToArray()
				.Where(t => t.Status == status)
				.FirstOrDefault().SomeNotNull();

		protected override IQueryable<TalkDTO> Include(IQueryable<TalkDTO> src) => src
			.Include(x => x.City)
			.Include(x => x.Place.City)
			.Include(x => x.Speaker.City)
			.Include(x => x.ApprovedBy.City)
			.Include(x => x.PlaceVerifiedBy.City)
			.Include(x => x.PublishedBy.City)
			.Include(x => x.ReportedBy.City);
	}
}
