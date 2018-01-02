using System;

namespace FreePeople.Persistence.DTO
{
	public class TalkDTO
	{
		public TalkDTO(Guid id,
			string name, string comment, string shortInfo, string fullInfo,
			DateTime startsAt,
			Guid speakerId, Guid cityId, string report,
			DateTime? approvedAt,
			Guid? approvedById,
			Guid? placeId,
			DateTime? placeVerifiedAt,
			Guid? placeVerifiedById,
			DateTime? publishedAt,
			Guid? publishedById,
			DateTime? reportedAt,
			Guid? reportedById)
		{
			Id = id;
			Name = name;
			Comment = comment;
			ShortInfo = shortInfo;
			FullInfo = fullInfo;
			StartsAt = startsAt;
			SpeakerId = speakerId;
			CityId = cityId;
			Report = report;
			ApprovedAt = approvedAt;
			ApprovedById = approvedById;
			PlaceId = placeId;
			PlaceVerifiedAt = placeVerifiedAt;
			PlaceVerifiedById = placeVerifiedById;
			PublishedAt = publishedAt;
			PublishedById = publishedById;
			ReportedAt = reportedAt;
			ReportedById = reportedById;
		}

		public TalkDTO() { }

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Comment { get; set; }
		public string ShortInfo { get; set; }
		public string FullInfo { get; set; }
		public DateTime StartsAt { get; set; }
		public Guid SpeakerId { get; set; }
		public SpeakerDTO Speaker { get; set; }
		public Guid CityId { get; set; }
		public CityDTO City { get; set; }
		public string Report { get; set; }
		public DateTime? ApprovedAt { get; set; }
		public Guid? ApprovedById { get; set; }
		public AdministratorDTO ApprovedBy { get; set; }
		public Guid? PlaceId { get; set; }
		public PlaceDTO Place { get; set; }
		public DateTime? PlaceVerifiedAt { get; set; }
		public Guid? PlaceVerifiedById { get; set; }
		public AdministratorDTO PlaceVerifiedBy { get; set; }
		public DateTime? PublishedAt { get; set; }
		public Guid? PublishedById { get; set; }
		public AdministratorDTO PublishedBy { get; set; }
		public DateTime? ReportedAt { get; set; }
		public Guid? ReportedById { get; set; }
		public AdministratorDTO ReportedBy { get; set; }
	}
}
