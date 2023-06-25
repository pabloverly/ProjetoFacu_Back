using ApiTools.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecaptcharController : ControllerBase
    {
        private readonly RecaptchaHelper _recaptchaHelper;

        public RecaptcharController(RecaptchaHelper recaptchaHelper)
        {
            _recaptchaHelper = recaptchaHelper;
        }

        [HttpPost("verificar")]
        public async Task<IActionResult> Verificar([FromBody] string token)
        {
            bool success = await _recaptchaHelper.VerifyRecaptcha(token);

            if (success)
            {
                // O reCAPTCHA foi verificado com sucesso, faça o que for necessário aqui
                return Ok("reCAPTCHA verificado com sucesso!");
            }
            else
            {
                // O reCAPTCHA não foi verificado com sucesso, lide com o erro aqui
                return BadRequest("Falha na verificação do reCAPTCHA.");
            }
        }
    }
}


