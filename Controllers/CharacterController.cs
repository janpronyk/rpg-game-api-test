using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rpg_game.Dtos.Character;
using rpg_game.models;
using rpg_game.services.CharacterService;

namespace rpg_game.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }

        [HttpGet("get-all")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            int id = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return Ok(await _characterService.GetAllCharacters(id));
        }

        [HttpGet("get-one/:{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetOne(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut("update-one")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacterById(UpdateCharacterDto updatedCharacter) 
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            if(response.Data == null) {
                return NotFound(response);
            }
            return response;
        }

        [HttpDelete("delete-one/:{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacterById(int id)
        {
             var response = await _characterService.DeleteCharacter(id);
            if(response.Data == null) {
                return NotFound(response);
            }
            return response;
        }
    }
}