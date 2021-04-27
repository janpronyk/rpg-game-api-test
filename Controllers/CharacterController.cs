using Microsoft.AspNetCore.Mvc;
using rpg_game.models;

namespace rpg_game.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();


        [HttpGet]
        public ActionResult<Character> Get()
        {
            return Ok(knight);
        }
    }
}