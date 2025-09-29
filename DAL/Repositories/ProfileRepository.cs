using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly Su25pantherDbContext _context;

        public ProfileRepository(Su25pantherDbContext context)
        {
            _context = context;
        }

        private IQueryable<PantherProfile> BuildSearchQuery(double? weight, string? pantherTypeName)
        {
            var query = _context.PantherProfiles.Include(p => p.PantherTypes).AsQueryable();

            // OR logic
            if (weight.HasValue || !string.IsNullOrWhiteSpace(pantherTypeName))
            {
                query = query.Where(p => (weight.HasValue && p.Weight == weight.Value) ||
                                         (!string.IsNullOrWhiteSpace(pantherTypeName) && p.PantherTypes.PantherTypeName.Contains(pantherTypeName)));
            }

            return query;
        }

        public async Task<int> CountProfilesAsync()
        {
            return await _context.PantherProfiles.CountAsync();
        }

        public async Task<int> CountSearchProfilesAsync(double? weight, string? pantherTypeName)
        {
            return await BuildSearchQuery(weight, pantherTypeName).CountAsync();
        }

        public async Task<int> CreateProfileAsync(PantherProfile entity)
        {
            _context.PantherProfiles.Add(entity);
            await _context.SaveChangesAsync();
            return entity.PantherProfileId;
        }

        public async Task DeleteProfileAsync(int id)
        {
            var entity = await _context.PantherProfiles.FindAsync(id);
            if (entity == null) return;

            _context.PantherProfiles.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PantherType>> GetAllPantherTypesAsync()
        {
            return await _context.PantherTypes
                                 .OrderBy(t => t.PantherTypeName)
                                 .ToListAsync();
        }

        public async Task<PantherProfile?> GetByIdAsync(int id)
        {
            return await _context.PantherProfiles
                                 .Include(p => p.PantherTypes)
                                 .FirstOrDefaultAsync(p => p.PantherProfileId == id);
        }

        public async Task<List<PantherProfile>> GetProfilesAsync(int page, int pageSize)
        {
            return await _context.PantherProfiles
                                 .Include(p => p.PantherTypes)
                                 .OrderByDescending(p => p.ModifiedDate)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<List<PantherProfile>> SearchProfilesAsync(double? weight, string? pantherTypeName, int page, int pageSize)
        {
            return await BuildSearchQuery(weight, pantherTypeName)
                .OrderByDescending(p => p.ModifiedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task UpdateProfileAsync(int id, PantherProfile dto)
        {
            var entity = await _context.PantherProfiles.FindAsync(id);
            if (entity == null) return;

            entity.PantherTypeId = dto.PantherTypeId;
            entity.PantherName = dto.PantherName.Trim();
            entity.Weight = dto.Weight;
            entity.Characteristics = dto.Characteristics.Trim();
            entity.Warning = dto.Warning.Trim();
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
