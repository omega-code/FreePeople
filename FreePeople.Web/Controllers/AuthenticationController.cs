using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreePeople.Domain;
using FreePeople.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreePeople.Web.Controllers
{
    public class AuthenticationController : Controller
    {
		private readonly SpeakerService _speakerService;

		public AuthenticationController(SpeakerService speakerService)
		{
			_speakerService = speakerService;
		}

		public IActionResult Speaker() => View(new AuthenticateSpeakerCommand());

		[HttpPost]
		public IActionResult Speaker(AuthenticateSpeakerCommand model)
		{
			if (!ModelState.IsValid)
				return View();

			_speakerService.Welcome(model.Email);
			return View("SpeakerCheckEmail");
		}


		public IActionResult Administrator() => View(new AuthenticateAdministratorCommand());

		[HttpPost]
		public IActionResult Administrator(AuthenticateAdministratorCommand model)
		{
			if (!ModelState.IsValid)
				return View();

			return View();
		}
	}
}