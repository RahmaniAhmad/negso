using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using repositoryPattern.Business;
using repositoryPattern.Entities;
using repositoryPattern.Api.Models;
using repositoryPattern.Api.Services;

namespace repositoryPattern.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenFactoryService _tokenFactoryService;

        public AuthController(IUserService userService, ITokenFactoryService tokenFactoryService)
        {
            _userService = userService;
            _tokenFactoryService = tokenFactoryService;
        }
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] Login model)
        {
            if (model == null)
            {
                return BadRequest("user is not set.");
            }

            var user = _userService.Find(model.Email, model.Password);
            if (user == null)
            {
                return BadRequest("Invalid email or password.");
            }

            var jwt = _tokenFactoryService.CreateAccessToken(user);
            return Ok(new { access_token = jwt });
        }
    }
}