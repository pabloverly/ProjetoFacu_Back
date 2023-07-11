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
        private readonly AppDbContext _appDbcontext;
        public Autenticate(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        [HttpPost]
        [Route("valid")]
        [AllowAnonymous]
        // [Authorize(AuthenticationSchemes = "Bearer")] //para obrigar o uso do bearer 
        public async Task<ActionResult<dynamic>> Authenticate([FromQuery] string username)
        {
            List<Contact> contactModels = new List<Contact>();

            // var data = await _appDbcontext.Contact.Where(x => x.Username.Contains(model)).ToListAsync();
            try
            {
                contactModels = await _appDbcontext.Contact.Where(x => x.Username.Equals(username)).ToListAsync();
                if (contactModels[0].Username == null)
                    return NotFound(new { message = "Usuário inválidos" });

                var token = TokenAutenticeService.GenerateToken(username);
                // model.Token = "";
                _appDbcontext.SessionValid.Add(new SessionValid
                {
                    Token = token,
                    CreateAt = DateTime.Now,
                    Valid = DateTime.Now.AddHours(2),
                    sn_ativo = 1
                });
                await _appDbcontext.SaveChangesAsync();

                return new
                {
                    contact = contactModels[0].Username,
                    token = token
                };
            }
            catch (Exception ex)
            {
                return NotFound(new { message = "Usuário inválidos" });
            }

            // Contact contact = new Contact();
            // contact = ContactRepository.Get(model);



            // return Ok
            //   (new
            //   {
            //       sucess = true,
            //       data = await _appDbcontext.Contact.ToListAsync()
            //   }
            //   );
        }

        [HttpGet]
        public async Task<IActionResult> GetContatct([FromQuery] string token)
        {

            if (token != null)
            {
                try
                {
                    List<SessionValid> sessionModels = new List<SessionValid>();
                    sessionModels = await _appDbcontext.SessionValid.Where(x => x.Token.Equals(token)).ToListAsync();

                    if (sessionModels[0].sn_ativo == 0)
                        return NotFound(new { message = "Token inválidos" }
                    );
                    else

                        return Ok
                        (new
                        {
                            sucess = true,
                            data = sessionModels
                        }
                        );
                }
                catch (Exception ex)
                {
                    return Ok(false);
                }
            }
            else
            {
                return NotFound
                (new
                {
                    sucess = false,
                    message = "Não encontrado"
                }
                );
            }

        }


        // [HttpGet]
        // public async Task<IActionResult> GetContatct([FromQuery] int? ContactId, [FromQuery] string? Username)
        // {
        //     if (Username != null)
        //     {
        //         return Ok
        //         (new
        //         {
        //             sucess = true,
        //             data = await _appDbcontext.Contact.Where(x => x.Username.Contains(Username)).ToListAsync()
        //         }
        //         );
        //     }
        //     else
        //     {
        //         var contact = await _appDbcontext.Contact.FindAsync(ContactId);
        //         if (contact == null)
        //         {
        //             return NotFound
        //             (new
        //             {
        //                 sucess = false,
        //                 message = "Item não encontrado"
        //             }
        //             );
        //         }
        //         return Ok
        //         (new
        //         {
        //             sucess = true,
        //             data = contact
        //         }
        //         );
        //     }

        // }


    }
}