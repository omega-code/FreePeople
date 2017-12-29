using System;
using System.Collections.Generic;
using System.Text;
using FreePeople.Domain;
using FreePeople.Persistence.DTO;
using Optional;
using Optional.Linq;

namespace FreePeople.Persistence.Repositories
{
	public class SpeakerRepository : RepositoryBase<Speaker, Guid, SpeakerDTO, SpeakerFactory>, ISpeakerRepository
	{
		public SpeakerRepository(SpeakerFactory factory, string connectionString) : base(nameof(SpeakerDTO.Id), factory, connectionString)
		{
		}

		public Option<Speaker> FindByEmail(string email) => Find(s => s.Email == email);
	}
}
