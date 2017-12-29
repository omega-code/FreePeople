using System;

namespace FreePeople.Persistence.DTO
{
	public class CityDTO
    {
		public CityDTO(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public CityDTO() { }

		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
