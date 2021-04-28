using AutoMapper;
using rpg_game.Dtos.Character;
using rpg_game.models;

namespace rpg_game
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
        }
    }
}