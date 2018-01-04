using System;
using System.Collections.Generic;
using System.Linq;
using FreePeople.Domain;
using Optional;

namespace FreePeople.Web.Models
{
	public class MakeDraftView
	{
		public MakeDraftView(Guid speakerId, string speakerEmail, Option<Talk> talk, Option<Speaker> speaker, IReadOnlyCollection<City> cities, IReadOnlyCollection<DateTime> mondays) : this(
			speakerId,
			speaker
				.Map(s => new CreateTalkCommand
				{
					CityId = s.City.Id,
					SpeakerName = s.Name,
					SpeakerAbout = s.About,
					SpeakerEmail = s.Email,
					SpeakerPhoto = s.Photo,
					SpeakerPhone = s.Phone.ValueOr((string)null),
					SpeakerFacebook = s.Facebook.ValueOr((string)null),
					TalkName = talk.Map(t => t.Name).ValueOr(() => TalkNamePattern(s.Name.SomeNotNull())),
					TalkComment = talk.Map(t => t.Comment).ValueOr(s.About),
					TalkStartsAt = talk.Map(t => (DateTime?)t.StartsAt).ValueOr((DateTime?)null),
					TalkShortInfo = talk.Map(t => t.ShortInfo).ValueOr((string)null),
					TalkFullInfo = talk.Map(t => t.FullInfo).ValueOr((string)null)
				})
				.ValueOr(new CreateTalkCommand { SpeakerEmail = speakerEmail, TalkName = TalkNamePattern(Option.None<string>()) }),
			cities, mondays
		) { }

		public MakeDraftView(Guid speakerId, CreateTalkCommand cmd, IReadOnlyCollection<City> cities, IReadOnlyCollection<DateTime> mondays)
		{
			Cities = cities.Select(CityView.FromDomain).ToArray();
			SpeakerId = speakerId;
			SpeakerEmail = cmd.SpeakerEmail;
			Command = cmd;
			Mondays = mondays;
		}

		public CreateTalkCommand Command { get; }
		public IReadOnlyCollection<DateTime> Mondays { get; }

		private static string TalkNamePattern(Option<string> speakerName) => $"Public Talk с {speakerName.ValueOr("...")}";

		public CityView[] Cities { get; }
		public Guid SpeakerId { get; }
		public string SpeakerEmail { get; }
	}
}
