using System;
using FreePeople.Domain;

namespace FreePeople.Web.Models
{
	public class AdministratorView
	{
		public AdministratorView(Guid id, CityView city, string email, string name)
		{
			Id = id;
			City = city;
			Email = email;
			Name = name;
		}

		public Guid Id { get; }
		public CityView City { get; }
		public string Email { get; }
		public string Name { get; }

		public static AdministratorView FromDomain(Administrator e) => new AdministratorView(e.Id, CityView.FromDomain(e.City), e.Email, e.Name);
	}
}