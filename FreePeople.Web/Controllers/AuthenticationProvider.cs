using System;
using System.Security.Claims;
using FreePeople.Domain;
using Optional;

namespace FreePeople.Web.Controllers
{
	public class AuthenticationProvider
	{
		public Option<Administrator> SomeIfAdministrator(ClaimsPrincipal user)
		{
			//TODO: implement
			return new Administrator(Guid.Empty, new City(Guid.Parse("8ae383e1-d0b3-4d2a-b7b8-d117a477fa66"), "Москва"), "Верховный магистр", "master@freepeople.world").SomeNotNull();
		}
	}
}