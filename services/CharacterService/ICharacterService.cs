using System.Collections.Generic;
using System.Threading.Tasks;
using rpg_game.Dtos.Character;
using rpg_game.models;

namespace rpg_game.services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId);

        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);

        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (UpdateCharacterDto updatedCharacter);
    
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id);
    }
}