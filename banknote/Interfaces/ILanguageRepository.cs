using System.Collections.Generic;
using System.Threading.Tasks;
using banknote.Models;
using banknote.ViewModels;

namespace banknote.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}