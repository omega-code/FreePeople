using System;
using FreePeople.Domain.Infrastructure;

namespace FreePeople.Domain
{
	public interface ICityRepository : IEntityRepository<City, Guid> { }
}