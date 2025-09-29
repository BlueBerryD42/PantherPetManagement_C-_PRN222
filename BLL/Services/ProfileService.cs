using BLL.Dtos;
using DAL.Models;
using DAL.Repositories;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private const int DefaultPageSize = 3; 

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<List<PantherType>> GetAllPantherTypesAsync()
            => await _profileRepository.GetAllPantherTypesAsync();

        public async Task<PantherProfile?> GetByIdAsync(int id)
            => await _profileRepository.GetByIdAsync(id);

        public async Task<List<PantherProfile>> GetProfilesAsync(int page)
            => await _profileRepository.GetProfilesAsync(page, DefaultPageSize);

        public async Task<List<PantherProfile>> GetProfilesAsync(int page, int pageSize)
            => await _profileRepository.GetProfilesAsync(page, pageSize);

        public async Task<List<PantherProfile>> SearchProfilesAsync(double? weight, string? pantherTypeName, int page)
            => await _profileRepository.SearchProfilesAsync(weight, pantherTypeName, page, DefaultPageSize);

        public async Task<List<PantherProfile>> SearchProfilesAsync(double? weight, string? pantherTypeName, int page, int pageSize)
            => await _profileRepository.SearchProfilesAsync(weight, pantherTypeName, page, pageSize);

        public async Task<int> CountSearchProfilesAsync(double? weight, string? pantherTypeName)
            => await _profileRepository.CountSearchProfilesAsync(weight, pantherTypeName);

        public async Task<int> CountProfilesAsync()
            => await _profileRepository.CountProfilesAsync();

        public async Task<int> CreateProfileAsync(ProfileDto dto)
        {
            var entity = new PantherProfile
            {
                PantherTypeId = dto.PantherTypeId,
                PantherName = dto.PantherName?.Trim() ?? string.Empty,
                Weight = dto.Weight,
                Characteristics = dto.Characteristics?.Trim() ?? string.Empty,
                Warning = dto.Warning?.Trim() ?? string.Empty,
                ModifiedDate = DateTime.Now
            };
            return await _profileRepository.CreateProfileAsync(entity);
        }

        public async Task UpdateProfileAsync(int id, ProfileDto dto)
        {
            var entity = new PantherProfile
            {
                PantherTypeId = dto.PantherTypeId,
                PantherName = dto.PantherName?.Trim() ?? string.Empty,
                Weight = dto.Weight,
                Characteristics = dto.Characteristics?.Trim() ?? string.Empty,
                Warning = dto.Warning?.Trim() ?? string.Empty,
                ModifiedDate = DateTime.Now
            };
            await _profileRepository.UpdateProfileAsync(id, entity);
        }

        public async Task DeleteProfileAsync(int id)
        {
            await _profileRepository.DeleteProfileAsync(id);
        }
    }
}