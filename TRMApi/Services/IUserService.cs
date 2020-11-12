using System.Threading.Tasks;
using TRMDataManager.Library.Models;

namespace TRMApi.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserByIdAsync(string id);
    }
}