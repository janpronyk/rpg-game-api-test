using System.Threading.Tasks;
using rpg_game.Dtos.Character;
using rpg_game.Dtos.Weapon;
using rpg_game.models;

namespace rpg_game.services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}