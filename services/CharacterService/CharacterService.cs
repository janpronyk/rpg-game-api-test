using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using rpg_game.Data;
using rpg_game.Dtos.Character;
using rpg_game.models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace rpg_game.services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        // private static List<Character> _characters = new List<Character> {
        //     new Character { Id = 1},
        //     new Character { Id = 2, Name = "Sam"},
        //     new Character { Id = 3, Name = "Sauron", HitPoints = 200, Class = RpgClass.Mage }
        // };
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }

        private int GetUserId() => int.Parse(
            _httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier)
        );

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character = await _context.Characters.FirstAsync(
                    c => c.Id == id && c.User.Id == GetUserId());
                
                if(character != null)
                {
                    _context.Remove(character);
                  
                        await _context.SaveChangesAsync();

                }
                else {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }

                serviceResponse.Data = await _context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            var query = await _context.Characters
                .Where(c => c.User.Id == GetUserId())
                .ToListAsync();

            serviceResponse.Data = query.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var query = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(query);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character character = _mapper.Map<Character>(newCharacter);
            // character.Id = _characters.Max(c => c.Id) + 1;
            // _characters.Add(character);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Characters.Add(character);

            try
            {

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            serviceResponse.Data = await _context.Characters
            .Where( c => c.User.Id == GetUserId())
            .Select( c => _mapper.Map<GetCharacterDto>(c))
            .ToListAsync();

            return serviceResponse;
        }
    }
}