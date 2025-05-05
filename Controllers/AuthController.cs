using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using api.Dtos;
using api.Mappers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;


namespace api.Controllers {
    
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IUserRepository userRepo) : ControllerBase {

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDto request) {
           
             var result = await userRepo.Register(request);
           
           return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDto request) {
            var result = await userRepo.Login(request);
            if(result == null) {
                return BadRequest("Invalid credentials");
            }
            return Ok(result);
        }
    }

}
