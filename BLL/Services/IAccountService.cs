using DAL.Models;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IAccountService
    {
        Task<PantherAccount?> LoginAsync(string email, string password);
    }
}
