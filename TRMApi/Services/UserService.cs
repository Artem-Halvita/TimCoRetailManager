using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMApi.Repository;
using TRMDataManager.Library.Models;

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
