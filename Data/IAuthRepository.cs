using System.Threading.Tasks;
using rpg_game.models;

namespace rpg_game.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string passowrd);

        Task<ServiceResponse<string>> Login(string username, string password);

        Task<bool> UserExists(string username);
        
    }
}