using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories
{


public interface IProfileRepository
{
    Task<List<PantherType>> GetAllPantherTypesAsync();
    Task<PantherProfile?> GetByIdAsync(int id);
    Task<List<PantherProfile>> GetProfilesAsync(int page, int pageSize);
    Task<List<PantherProfile>> SearchProfilesAsync(double? weight, string? pantherTypeName, int page, int pageSize);
    Task<int> CountSearchProfilesAsync(double? weight, string? pantherTypeName);
    Task<int> CountProfilesAsync();
    Task<int> CreateProfileAsync(PantherProfile entity);
    Task UpdateProfileAsync(int id, PantherProfile entity);
    Task DeleteProfileAsync(int id);
}

}
