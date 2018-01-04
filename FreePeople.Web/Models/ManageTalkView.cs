using System;
using System.Collections.Generic;
using Optional;

namespace FreePeople.Web.Models
{
	public class ManageTalkView
	{
		public ManageTalkView(TalkView talk, IReadOnlyCollection<PlaceView> places) : 
			this(
				talk,
				new EditTalkCommand
				{
					SpeakerName = talk.Name,
					SpeakerAbout = talk.Speaker.About,
					SpeakerEmail = talk.Speaker.Email,
					SpeakerPhoto = talk.Speaker.Photo,
					SpeakerPhone = talk.Speaker.Phone.ValueOr((string)null),
					SpeakerFacebook = talk.Speaker.Facebook.ValueOr((string)null),
					TalkName = talk.Name,
					TalkComment = talk.Comment,
					TalkStartsAt = talk.StartsAt,
					TalkShortInfo = talk.ShortInfo,
					TalkFullInfo = talk.FullInfo
				}, 
				places
			) { }

		public ManageTalkView(TalkView talk, EditTalkCommand cmd, IReadOnlyCollection<PlaceView> places)
		{
			TalkId = talk.Id;
			Talk = talk;
			Command = cmd;
			Places = places;
		}

		public IReadOnlyCollection<PlaceView> Places { get; }
		public Guid TalkId { get; }
		public TalkView Talk { get; }
		public EditTalkCommand Command { get; }
	}
}
