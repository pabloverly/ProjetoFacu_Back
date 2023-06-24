using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTools.Model;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ApiTools.Services;
using ApiTools.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("jwt/account")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        // [Authorize(AuthenticationSchemes = "Bearer")] //para obrigar o uso do bearer 
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
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