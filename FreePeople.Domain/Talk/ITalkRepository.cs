using System;
using System.Collections.Generic;
using FreePeople.Domain.Infrastructure;
using Optional;

namespace FreePeople.Domain
{
	public interface ITalkRepository : IEntityRepository<Talk, Guid>
	{
		Option<Talk> FindTalkFor(Guid speakerId, TalkStatus status);
		IReadOnlyCollection<Talk> List(DateTime from, DateTime to, Guid id);
	}
}