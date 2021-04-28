using System.Collections.Generic;
using System.Threading.Tasks;
using rpg_game.models;

namespace rpg_game.services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> GetAllCharacters();

        Task<ServiceResponse<Character>> GetCharacterById(int id);

        Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter);
    }
}