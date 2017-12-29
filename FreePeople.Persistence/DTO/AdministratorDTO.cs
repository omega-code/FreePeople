using System;

namespace FreePeople.Persistence.DTO
{
	internal class AdministratorDTO
	{
		public AdministratorDTO(Guid id, Guid cityId, string name, string email)
		{
			Id = id;
			CityId = cityId;
			Name = name;
			Email = email;
		}

		public AdministratorDTO() { }

		public Guid Id { get; set; }
		public Guid CityId { get; set; }
		public CityDTO City { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
