using banknote.Data;
using banknote.Repository;
using banknote.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace banknote.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context = null;

        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await _context.Language.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name
            }).ToListAsync();
        }
    }
}
