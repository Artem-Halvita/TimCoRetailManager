using System.Threading.Tasks;
using TRMApi.Data.Models;

namespace TRMApi.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserByIdAsync(string id);
    }
}