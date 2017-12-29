using System;
using Optional;

namespace FreePeople.Domain
{
	public class SpeakerService
	{
		private readonly ISpeakerRepository _speakerRepository;
		private readonly IEmailService _emailService;
		private readonly string _rootUrl;

		public SpeakerService(ISpeakerRepository speakerRepository, IEmailService emailService, string rootUrl)
		{
			_speakerRepository = speakerRepository;
			_emailService = emailService;
			_rootUrl = rootUrl;
		}

		public void Welcome(string email)
		{
			var speaker = _speakerRepository.FindByEmail(email);
			var speakerId = speaker.Map(s => s.Id).ValueOr(() => Guid.NewGuid());

			_emailService.Send(email, "info@freepeople.world", "Приглашение на выступление от Люди Вне Профессии", PrepareSpeakerWelcomeText(speakerId, speaker));
		}

		private string PrepareSpeakerWelcomeText(Guid speakerId, Option<Speaker> speaker) => $@"
			<p>Добрый день, {speaker.Map(x => x.Name).ValueOr("дорогой спикер")}! </p>

			<p>Для внесения информации о себе и вашем выступлении, перейдите по ссылке: </p>

			<p>{_rootUrl}/talk/?speakerId={speakerId}</p>

			<p>С уважением, команда «Люди Вне Профессии»</p>
";
	}
}