using System;
using System.Threading.Tasks;
using efwebapi.Models;
using efwebapi.Repositories;
using efwebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace efwebapi.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);
            if(user == null)
                return NotFound(new { message = "Usuário inválido ou senha incorreta" });
            
            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return new {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";


        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authentitated() => String.Format("{0} está autenticado", User.Identity.Name);



        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Employee";


        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Manager";


    }
}