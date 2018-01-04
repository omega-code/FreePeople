using System;
using FreePeople.Domain;

namespace FreePeople.Web.Models
{
	public class TalkView
	{
		public TalkView(
			Guid id, CityView city, SpeakerView speaker, DateTime startsAt, TalkStatus status, 
			string name, string comment, string shortInfo, string fullInfo, 
			DateTime? approvedAt, AdministratorView approvedBy, 
			DateTime? placeVerifiedAt, AdministratorView placeVerifiedBy, PlaceView place, 
			DateTime? publishedAt, AdministratorView publishedBy, 
			DateTime? reportedAt, AdministratorView reportedBy, string report)
		{
			Id = id;
			City = city;
			Speaker = speaker;
			StartsAt = startsAt;
			Status = status;
			Name = name;
			Comment = comment;
			ShortInfo = shortInfo;
			FullInfo = fullInfo;
			ApprovedAt = approvedAt;
			ApprovedBy = approvedBy;
			PlaceVerifiedAt = placeVerifiedAt;
			PlaceVerifiedBy = placeVerifiedBy;
			Place = place;
			PublishedAt = publishedAt;
			PublishedBy = publishedBy;
			ReportedAt = reportedAt;
			ReportedBy = reportedBy;
			Report = report;
		}

		public Guid Id { get; }
		public CityView City { get; }
		public SpeakerView Speaker { get; }
		public DateTime StartsAt { get; }
		public TalkStatus Status { get; }
		public string Name { get; }
		public string Comment { get; }
		public string ShortInfo { get; }
		public string FullInfo { get; }
		public DateTime? ApprovedAt { get; }
		public AdministratorView ApprovedBy { get; }
		public DateTime? PlaceVerifiedAt { get; }
		public AdministratorView PlaceVerifiedBy { get; }
		public PlaceView Place { get; }
		public DateTime? PublishedAt { get; }
		public AdministratorView PublishedBy { get; }
		public DateTime? ReportedAt { get; }
		public AdministratorView ReportedBy { get; }
		public string Report { get; }

		public static TalkView FromDomain(Talk talk) => new TalkView(
			talk.Id, CityView.FromDomain(talk.City), SpeakerView.FromDomain(talk.Speaker), 
			talk.StartsAt, talk.Status,
			talk.Name, talk.Comment, talk.ShortInfo, talk.FullInfo,
			talk.ApprovedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null),
			talk.ApprovedBy.Map(AdministratorView.FromDomain).ValueOr((AdministratorView)null),
			talk.PlaceVerifiedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null),
			talk.PlaceVerifiedBy.Map(AdministratorView.FromDomain).ValueOr((AdministratorView)null),
			talk.Place.Map(PlaceView.FromDomain).ValueOr((PlaceView)null),
			talk.PublishedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null),
			talk.PublishedBy.Map(AdministratorView.FromDomain).ValueOr((AdministratorView)null),
			talk.ReportedAt.Map(x => (DateTime?)x).ValueOr((DateTime?)null),
			talk.ReportedBy.Map(AdministratorView.FromDomain).ValueOr((AdministratorView)null),
			talk.Report.ValueOr((string)null)
		);

	}
}