using FreePeople.Domain.Infrastructure;
using Optional;

namespace FreePeople.Domain
{
	public class AdministratorService
	{
		private readonly IAdministratorRepository _administratorRepository;
		private readonly IEmailService _emailService;
		private readonly string _rootUrl;

		public AdministratorService(IAdministratorRepository administratorRepository, IEmailService emailService, string rootUrl)
		{
			_administratorRepository = administratorRepository;
			_emailService = emailService;
			_rootUrl = rootUrl;
		}

		public Option<Administrator> CheckUser(string email, string pin) => _administratorRepository
			.FindByEmail(email)
			.Filter(x => x.Id.ToString().ToLowerInvariant().StartsWith(pin.ToLowerInvariant()));
	}
}