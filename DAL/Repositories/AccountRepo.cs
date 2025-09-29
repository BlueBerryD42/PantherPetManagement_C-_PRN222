using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private Su25pantherDbContext _su25PantherDbContext;

        public AccountRepo(Su25pantherDbContext context)
        {
            _su25PantherDbContext = context;
        }

        public async Task<PantherAccount?> LoginAsync(string email, string password)
        {
            return await _su25PantherDbContext.PantherAccounts
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}
