using System;
using System.Collections.Generic;
using System.Linq;
using Optional;

namespace FreePeople.Domain
{
	public class TalkService
	{
		private readonly ITalkRepository _talkRepository;
		private readonly ISpeakerRepository _speakerRepository;
		private readonly ICityRepository _cityRepository;

		public TalkService(ITalkRepository talkRepository, ISpeakerRepository speakerRepository, ICityRepository cityRepository)
		{
			_talkRepository = talkRepository;
			_speakerRepository = speakerRepository;
			_cityRepository = cityRepository;
		}

		public Option<Talk> FindCurrentDraftFor(Guid speakerId) => 
			_talkRepository.FindTalkFor(speakerId, TalkStatus.Draft);

		public Talk MakeDraft(
			Guid speakerId, Guid cityId, string speakerName, byte[] speakerPhoto, string speakerAbout, 
			string speakerEmail, Option<string> facebook, Option<string> phone,
			DateTime startsAt, string talkName, string talkComment, string shortInfo, string fullInfo
		)
		{
			if (startsAt.DayOfWeek != DayOfWeek.Monday)
			{
				throw new ArgumentException("UI should accept only Mondays");
			}

			var city = _cityRepository.GetByKey(cityId);
			var upToDateSpeaker = _speakerRepository.FindByKey(speakerId)
				.Map(speaker =>
				{
					speaker.Name = speakerName;
					speaker.Photo = speakerPhoto;
					speaker.About = speakerAbout;
					speaker.Facebook = facebook;
					speaker.Phone = phone;
					_speakerRepository.SaveOrUpdate(speaker);
					return speaker;
				})
				.ValueOr(() =>
				{
					var speaker = new Speaker(speakerId, city, speakerName, speakerPhoto, speakerAbout, speakerEmail, facebook, phone);
					_speakerRepository.SaveOrUpdate(speaker);
					return speaker;
				});


			var upToDateTalk = FindCurrentDraftFor(speakerId)
				.Map(talk =>
				{
					talk.EditDraft(startsAt, city, talkName, talkComment, shortInfo, fullInfo);
					_talkRepository.SaveOrUpdate(talk);
					return talk;
				})
				.ValueOr(() =>
				{
					var talk = new Talk(Guid.NewGuid(), city, startsAt, upToDateSpeaker, talkName, talkComment, shortInfo, fullInfo);
					_talkRepository.SaveOrUpdate(talk);
					return talk;
				});
			return upToDateTalk;
		}

		public IReadOnlyCollection<DateTime> GetNearestMondays() => 
			GetNearestMondaysInner().ToArray();

		private IEnumerable<DateTime> GetNearestMondaysInner()
		{
			var monday = DateTime.Today.AddDays(DayOfWeek.Monday - DateTime.Today.DayOfWeek);
			for (int i = 0; i < 6; i++)
			{
				yield return monday.AddDays(i * 7);
			}
		}

		public IReadOnlyCollection<Talk> List(DateTime from, DateTime to, City city) => 
			_talkRepository.List(from, to, city.Id);

		public Talk GetById(Guid id) =>
			_talkRepository.GetByKey(id);
	}
}