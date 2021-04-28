using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rpg_game.models;

namespace rpg_game.services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> _characters = new List<Character> {
            new Character { Id = 1},
            new Character { Id = 2, Name = "Sam"},
            new Character { Id = 3, Name = "Sauron", HitPoints = 200, Class = RpgClass.Mage }
        };

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            
            _characters.Add(newCharacter);
            serviceResponse.Data = _characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();

            serviceResponse.Data =_characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            
            serviceResponse.Data = _characters.FirstOrDefault(c => c.Id == id);
            return serviceResponse;
        }
    }
}