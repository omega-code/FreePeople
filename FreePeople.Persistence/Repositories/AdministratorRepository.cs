using System;
using System.Linq;
using FreePeople.Domain;
using FreePeople.Persistence.DTO;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace FreePeople.Persistence.Repositories
{
	public class AdministratorRepository : RepositoryBase<Administrator, Guid, AdministratorDTO, AdministratorFactory>, IAdministratorRepository
	{
		public AdministratorRepository(AdministratorFactory factory, string connectionString) : base(nameof(AdministratorDTO.Id), factory, connectionString)
		{
		}

		public Option<Administrator> FindByEmail(string email) => Find(s => s.Email == email);

		protected override IQueryable<AdministratorDTO> Include(IQueryable<AdministratorDTO> src) => src.Include(x => x.City);
	}
}
