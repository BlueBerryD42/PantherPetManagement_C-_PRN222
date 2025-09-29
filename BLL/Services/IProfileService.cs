
using BLL.Dtos;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IProfileService
    {
        Task<List<PantherType>> GetAllPantherTypesAsync();
        Task<PantherProfile?> GetByIdAsync(int id);
        Task<List<PantherProfile>> GetProfilesAsync(int page);
        Task<List<PantherProfile>> GetProfilesAsync(int page, int pageSize);
        Task<List<PantherProfile>> SearchProfilesAsync(double? weight, string? pantherTypeName, int page);
        Task<List<PantherProfile>> SearchProfilesAsync(double? weight, string? pantherTypeName, int page, int pageSize);
        Task<int> CountSearchProfilesAsync(double? weight, string? pantherTypeName);
        Task<int> CountProfilesAsync();
        Task<int> CreateProfileAsync(ProfileDto dto);
        Task UpdateProfileAsync(int id, ProfileDto dto);
        Task DeleteProfileAsync(int id);
    }
}
