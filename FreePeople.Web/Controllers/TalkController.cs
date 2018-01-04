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
		private readonly AuthenticationProvider _authenticationProvider;
		private readonly PlaceService _placeService;

		public TalkController(TalkService talkService, SpeakerService speakerService, CityService cityService, PlaceService placeService, AuthenticationProvider authenticationProvider)
		{
			_talkService = talkService;
			_speakerService = speakerService;
			_cityService = cityService;
			_placeService = placeService;
			_authenticationProvider = authenticationProvider;
		}

		public IActionResult MakeDraft(Guid speakerId, string speakerEmail) => View(
			new MakeDraftView(speakerId, speakerEmail, _talkService.FindCurrentDraftFor(speakerId), _speakerService.FindById(speakerId), _cityService.ListAll(), 
				_talkService.GetNearestMondays())
		);

		[HttpPost]
		public IActionResult MakeDraft(Guid speakerId, string speakerEmail, CreateTalkCommandWrapper wrapper)
		{
			var cmd = wrapper.Command;
			if (!ModelState.IsValid)
				return View(new MakeDraftView(speakerId, cmd, _cityService.ListAll(), _talkService.GetNearestMondays()));

			_talkService.MakeDraft(
				speakerId, cmd.CityId.Value, cmd.SpeakerName,
				cmd.SpeakerPhoto.SomeNotNull().ValueOr(() => new byte[0]),
				cmd.SpeakerAbout, cmd.SpeakerEmail, cmd.SpeakerFacebook.SomeNotNull(), cmd.SpeakerPhone.SomeNotNull(),
				cmd.TalkStartsAt.Value, cmd.TalkName, cmd.TalkComment, cmd.TalkShortInfo, cmd.TalkFullInfo
			);
			return View("SpeakerThankYou", new MakeDraftView(speakerId, cmd, _cityService.ListAll(), _talkService.GetNearestMondays()));
		}

		public IActionResult Calendar(int skip = 0) => _authenticationProvider.SomeIfAdministrator(User).Map(
			administrator =>
			{
				var now = DateTime.UtcNow;
				var nowMonth = new DateTime(now.Year, now.Month, 1);
				var month = nowMonth.AddMonths(-skip);
				var talks = _talkService.List(month, month.AddMonths(1), administrator.City);
				return (IActionResult)View(new TalkCalendarView(month, CityView.FromDomain(administrator.City), talks.Select(TalkView.FromDomain).ToArray()));
			}
		).ValueOr(() => RedirectToAction("Administrator", "Authentication"));


		public IActionResult Manage(Guid id) => _authenticationProvider.SomeIfAdministrator(User).Map(
			administrator =>
			{
				var talk = _talkService.GetById(id);
				return (IActionResult)base.View(
					new ManageTalkView(TalkView.FromDomain(talk),
					_placeService.ListFor(talk.City).Select(PlaceView.FromDomain).ToArray())
				);
			}
		).ValueOr(() => base.RedirectToAction("Administrator", "Authentication"));

		[HttpPost]
		public IActionResult Manage(Guid id, EditTalkCommandWrapper wrapper)
		{
			var cmd = wrapper.Command;
			var talk = _talkService.GetById(id);
			if (!ModelState.IsValid)
				return View(new ManageTalkView(TalkView.FromDomain(talk), cmd, _placeService.ListFor(talk.City).Select(PlaceView.FromDomain).ToArray()));

			throw new NotImplementedException();
			//_talkService.MakeDraft(
			//	speakerId, cmd.CityId.Value, cmd.SpeakerName,
			//	cmd.SpeakerPhoto.SomeNotNull().ValueOr(() => new byte[0]),
			//	cmd.SpeakerAbout, cmd.SpeakerEmail, cmd.SpeakerFacebook.SomeNotNull(), cmd.SpeakerPhone.SomeNotNull(),
			//	cmd.TalkStartsAt.Value, cmd.TalkName, cmd.TalkComment, cmd.TalkShortInfo, cmd.TalkFullInfo
			//);
			//return View("SpeakerThankYou", new MakeDraftView(speakerId, cmd, _cityService.ListAll()));
		}

	}
}