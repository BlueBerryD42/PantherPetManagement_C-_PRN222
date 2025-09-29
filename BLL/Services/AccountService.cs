using DAL.Models;
using DAL.Repository;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepository;

        public AccountService(IAccountRepo accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<PantherAccount?> LoginAsync(string email, string password)
        {
            return await _accountRepository.LoginAsync(email, password);
        }
    }
}
