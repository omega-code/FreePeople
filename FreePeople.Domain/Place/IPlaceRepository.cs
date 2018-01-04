using System;
using System.Collections.Generic;
using FreePeople.Domain.Infrastructure;

namespace FreePeople.Domain
{
	public interface IPlaceRepository : IEntityRepository<Place, Guid>
	{
		IReadOnlyCollection<Place> ListForCity(Guid id);
	}
}