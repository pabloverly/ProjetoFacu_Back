using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiTools.Context;
using ApiTools.Model;
using ApiTools.Repositories;
using ApiTools.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("call/[controller]")]

    public class Autenticate : Controller
    {
        private readonly AutentiteDbContext _appDbcontext;

        [HttpPost]
        [Route("valid")]
        [AllowAnonymous]
        // [Authorize(AuthenticationSchemes = "Bearer")] //para obrigar o uso do bearer 
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] string model)
        {

            Contact contact = new Contact();
            contact = ContactRepository.Get(model);

            if (contact == null)
                return NotFound(new { message = "Usuário inválidos" });

            var token = TokenAutenticeService.GenerateToken(model);
            // model.Token = "";

            // return new
            // {
            //     contact = contact,
            //     token = token
            // };

            return Ok
              (new
              {
                  sucess = true,
                  data = await _appDbcontext.Contact.ToListAsync()
              }
              );
        }

        [HttpGet]
        public async Task<IActionResult> GetContatct([FromQuery] int? ContactId, [FromQuery] string? description)
        {
            if (description != null)
            {
                return Ok
                (new
                {
                    sucess = true,
                    data = await _appDbcontext.Contact.Where(x => x.Username.Contains(description)).ToListAsync()
                }
                );
            }
            else
            {
                var contact = await _appDbcontext.Contact.FindAsync(ContactId);
                if (contact == null)
                {
                    return NotFound
                    (new
                    {
                        sucess = false,
                        message = "Item não encontrado"
                    }
                    );
                }
                return Ok
                (new
                {
                    sucess = true,
                    data = contact
                }
                );
            }

        }


    }
}