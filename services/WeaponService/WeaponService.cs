

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using rpg_game.Data;
using rpg_game.Dtos.Character;
using rpg_game.Dtos.Weapon;
using rpg_game.models;
using System.Security.Claims;

namespace rpg_game.services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = _context.Users.FirstOrDefault(c => c.Id == newWeapon.CharacterId);

                if (character == null)
                {
                    response.Success = false;
                        response.Message = "Character not found";
                        return response;
                    }

                    var weapon = new Weapon
                    {
                        Name = newWeapon.Name,
                        Damage = newWeapon.Damage,
                        Character = character
                    };

                    _context.Weapons.Add(weapon);
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}