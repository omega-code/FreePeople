using System;
using FreePeople.Domain.Infrastructure;
using Optional;

namespace FreePeople.Domain
{
	public interface IAdministratorRepository : IEntityRepository<Administrator, Guid>
	{
		Option<Administrator> FindByEmail(string email);
	}
}