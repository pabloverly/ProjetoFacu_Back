using Microsoft.AspNetCore.Mvc;
using ApiTools.Model;
using Microsoft.AspNetCore.Authorization;
using ApiTools.Services;
using ApiTools.Repositories;
using ApiTools.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("jwt/account")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _appDbcontext;
        public LoginController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        // [Authorize(AuthenticationSchemes = "Bearer")] //para obrigar o uso do bearer 
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            try
            {
                List<User> user = new List<User>();
                user = await _appDbcontext.User.Where(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password)).ToListAsync();

                if (user[0].Username == null || user == null)
                    return NotFound(new { message = "Usuário inválidos" });
                else
                {
                    var u = user[0].Username;
                    var token = TokenService.GenerateToken(model);
                    // user.Password = "";

                    return new
                    {
                        user = user,
                        token = token
                    };
                }
            }
            catch (Exception ex)
            {
                return BadRequest
                (new
                {
                    sucess = false,
                    message = ex.Message
                }
                );
            }

        }

        //para autenticacao anonima
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Acesso anônimo";

        //so usuarios autenticados
        [HttpGet]
        [Route("authenticated")]
        [Authorize] //ou [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        //so usuarios com a role employee ou manager
        [HttpGet]
        [Route("role")]
        [Authorize(Roles = "adm,user")] //acesso dos grupos
        public string Employee() => "Acesso permitido por grupo";

        //so usuarios com a role admin
        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Acesso admin";


    }
}