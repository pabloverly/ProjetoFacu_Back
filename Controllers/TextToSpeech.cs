using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiTools.Controllers
{
    [Route("[controller]")]
    public class TextToSpeech : Controller
    {
        private readonly ILogger<TextToSpeech> _logger;

        public TextToSpeech(ILogger<TextToSpeech> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("texttospeech")]
        [Authorize]
        public async Task SendText()
        {

        }


    }
}