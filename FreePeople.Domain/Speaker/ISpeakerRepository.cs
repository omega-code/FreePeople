using System;
using FreePeople.Domain.Infrastructure;
using Optional;

namespace FreePeople.Domain
{
	public interface ISpeakerRepository : IEntityRepository<Speaker, Guid>
	{
		Option<Speaker> FindByEmail(string email);
	}
}