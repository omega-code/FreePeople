using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FreePeople.Web.Controllers
{
    public class TalkController : Controller
    {
        public IActionResult CreateTalk(Guid speakerId)
        {
            return View();
        }
    }
}