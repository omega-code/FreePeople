using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreePeople.Domain;
using FreePeople.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Optional;

namespace FreePeople.Web.Controllers
{
    public class TalkController : Controller
    {
		private readonly TalkService _talkService;
		private readonly SpeakerService _speakerService;
		private readonly CityService _cityService;

		public TalkController(TalkService talkService, SpeakerService speakerService, CityService cityService)
		{
			_talkService = talkService;
			_speakerService = speakerService;
			_cityService = cityService;
		}

		public IActionResult MakeDraft(Guid speakerId, string speakerEmail) => View(
			new MakeDraftView(speakerId, speakerEmail, _talkService.FindCurrentDraftFor(speakerId), _speakerService.FindById(speakerId), _cityService.ListAll())
		);

		[HttpPost]
		public IActionResult MakeDraft(Guid speakerId, string speakerEmail, CreateTalkCommandWrapper wrapper)
		{
			var cmd = wrapper.Command;
			if (!ModelState.IsValid)
				return View(new MakeDraftView(speakerId, cmd, _cityService.ListAll()));

			_talkService.MakeDraft(
				speakerId, cmd.CityId.Value, cmd.SpeakerName, 
				cmd.SpeakerPhoto.SomeNotNull().ValueOr(() => new byte[0]), 
				cmd.SpeakerAbout, cmd.SpeakerEmail, cmd.SpeakerFacebook.SomeNotNull(), cmd.SpeakerPhone.SomeNotNull(),
				cmd.TalkStartsAt.Value, cmd.TalkName, cmd.TalkComment, cmd.TalkShortInfo, cmd.TalkFullInfo
			);
			return View("SpeakerThankYou", new MakeDraftView(speakerId, cmd, _cityService.ListAll()));
		}
	}
}