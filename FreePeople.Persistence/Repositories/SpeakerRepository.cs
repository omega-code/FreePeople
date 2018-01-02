using System;
using System.Linq;
using FreePeople.Domain;
using FreePeople.Persistence.DTO;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace FreePeople.Persistence.Repositories
{
	public class SpeakerRepository : RepositoryBase<Speaker, Guid, SpeakerDTO, SpeakerFactory>, ISpeakerRepository
	{
		public SpeakerRepository(SpeakerFactory factory, string connectionString) : base(nameof(SpeakerDTO.Id), factory, connectionString)
		{
		}

		public Option<Speaker> FindByEmail(string email) => Find(s => s.Email == email);

		protected override IQueryable<SpeakerDTO> Include(IQueryable<SpeakerDTO> src) => src.Include(x => x.City);
	}
}
