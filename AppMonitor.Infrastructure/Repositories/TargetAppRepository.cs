using AppMonitor.Application.Repositories;
using AppMonitor.Domain.Entities;
using AppMonitor.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace AppMonitor.Infrastructure.Services
{
    public class TargetAppRepository : ITargetAppRepository
    {
        private readonly TargetAppDbContext _context;

        public TargetAppRepository(TargetAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TargetApp>> GetAppsByUserAsync(string userId)
        {
            return await _context.Apps.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<TargetApp> GetAppByIdAsync(int id)
        {
            return await _context.Apps.FindAsync(id);
        }

        public async Task CreateAppAsync(TargetApp app)
        {
            await _context.Apps.AddAsync(app);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppAsync(TargetApp app)
        {
            _context.Apps.Update(app);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppAsync(int id)
        {
            var app = await _context.Apps.FindAsync(id);
            if (app != null)
            {
                _context.Apps.Remove(app);
                await _context.SaveChangesAsync();
            }
        }
    }
}
