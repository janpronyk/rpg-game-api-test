using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rpg_game.Dtos.Character;
using rpg_game.Dtos.Weapon;
using rpg_game.models;
using rpg_game.services.WeaponService;

namespace rpg_game.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;

        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }
    }
}