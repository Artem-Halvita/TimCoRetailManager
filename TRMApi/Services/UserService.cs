using System.Threading.Tasks;
using TRMApi.Data.Repository;
using TRMApi.Data.Models;

namespace TRMApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserModel, string> _userRepository;

        public UserService(IRepository<UserModel, string> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetUserByIdAsync(string id) => await _userRepository.GetByIdAsync(id);
    }
}
