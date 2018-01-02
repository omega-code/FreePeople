using Optional;
using System;

namespace FreePeople.Domain
{
	public enum TalkStatus
	{
		Draft,
		Approved,
		Placed,
		Published,
		Reported
	}

	public class Talk : Entity<Guid>
	{
		public Talk(Guid id, City city, DateTime startsAt, Speaker speaker, string name, string comment, string shortInfo, string fullInfo)
			: this(id,
				  name, comment, shortInfo, fullInfo, startsAt, speaker, city,
				  Option.None<DateTime>(), Option.None<Administrator>(),
				  Option.None<Place>(), Option.None<DateTime>(), Option.None<Administrator>(),
				  Option.None<DateTime>(), Option.None<Administrator>(), 
				  Option.None<string>(), Option.None<DateTime>(), Option.None<Administrator>()
			)
		{
		}

		internal Talk(Guid id,
			string name, string comment, string shortInfo, string fullInfo,
			DateTime startsAt,
			Speaker speaker, City city, 
			Option<DateTime> approvedAt,
			Option<Administrator> approvedBy,
			Option<Place> place,
			Option<DateTime> placeVerifiedAt,
			Option<Administrator> placeVerifiedBy,
			Option<DateTime> publishedAt,
			Option<Administrator> publishedBy,
			Option<string> report,
			Option<DateTime> reportedAt,
			Option<Administrator> reportedBy) : base(id)
		{
			Name = name;
			Comment = comment;
			ShortInfo = shortInfo;
			FullInfo = fullInfo;
			StartsAt = startsAt;
			Speaker = speaker;
			City = city;
			ApprovedAt = approvedAt;
			ApprovedBy = approvedBy;
			Place = place;
			PlaceVerifiedAt = placeVerifiedAt;
			PlaceVerifiedBy = placeVerifiedBy;
			PublishedAt = publishedAt;
			PublishedBy = publishedBy;
			Report = report;
			ReportedAt = reportedAt;
			ReportedBy = reportedBy;
		}

		public void EditDraft(DateTime startsAt, City city, string name, string comment, string shortInfo, string fullInfo)
		{
			StartsAt = startsAt;
			City = city;
			Name = name;
			Comment = comment;
			ShortInfo = shortInfo;
			FullInfo = fullInfo;
		}

		public string Name { get; private set; }
		public string Comment { get; private set; }
		public string ShortInfo { get; private set; }
		public string FullInfo { get; private set; }
		public TalkStatus Status => ApprovedAt.HasValue
			? Place.HasValue
				? PublishedAt.HasValue
					? ReportedAt.HasValue
						? TalkStatus.Reported
						: TalkStatus.Published
					: TalkStatus.Placed
				: TalkStatus.Approved
			: TalkStatus.Draft;

		public DateTime StartsAt { get; private set; }
		public Speaker Speaker { get; }
		public City City { get; private set; }
		public Option<Place> Place { get; private set; }
		public Option<DateTime> PlaceVerifiedAt { get; private set; }
		public Option<Administrator> PlaceVerifiedBy { get; private set; }
		public Option<string> Report { get; private set; }
		public Option<DateTime> ApprovedAt { get; private set; }
		public Option<Administrator> ApprovedBy { get; private set; }
		public Option<DateTime> PublishedAt { get; private set; }
		public Option<Administrator> PublishedBy { get; private set; }
		public Option<DateTime> ReportedAt { get; private set; }
		public Option<Administrator> ReportedBy { get; private set; }

		public void Approve(DateTime now, Administrator actor)
		{
			ApprovedAt = now.SomeNotNull();
			ApprovedBy = actor.SomeNotNull();
		}

		public void VerifyPlace(DateTime now, Administrator actor, Place place)
		{
			Place = place.SomeNotNull();
			PlaceVerifiedAt = now.SomeNotNull();
			PlaceVerifiedBy = actor.SomeNotNull();
		}

		public void Publish(DateTime now, Administrator actor)
		{
			PublishedAt = now.SomeNotNull();
			PublishedBy = actor.SomeNotNull();
		}

		public void GiveReport(DateTime now, Administrator actor, string reportText)
		{
			Report = reportText.SomeNotNull();
			ReportedAt = now.SomeNotNull();
			ReportedBy = actor.SomeNotNull();
		}
	}
}
