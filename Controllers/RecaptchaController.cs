using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTools.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("recaptcha")]
    public class RecaptchaController
    {
        [HttpPost]
        [Route("validate")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> VerifyRecaptcha([FromBody] Recaptcha recaptchaResponse)
        {

            var client = new HttpClient();
            var parameters = new Dictionary<string, string>
            {
                { "secret", "6LfZ9sgmAAAAAElW8nBjg2Pbkpz6Tr-755yDXazO" },
                { "response", recaptchaResponse.recaptchaResponse }
            };
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(parameters));
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseBody);
            return (bool)responseObject.success;
        }
    }
}