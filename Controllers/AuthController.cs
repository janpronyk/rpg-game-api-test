using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rpg_game.Data;
using rpg_game.Dtos.User;
using rpg_game.models;

namespace rpg_game.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;

        }


        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User { Username = request.Username}, request.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(UserLoginDto request)
        {
            var response = await _authRepo.Login(
                request.Username, request.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}