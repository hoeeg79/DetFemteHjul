using Infrastructure.Repositories;
using LanguageDto;

namespace Service;

public class LanguageService
{
   private readonly LanguageRepository _languageRepository;
   
   public LanguageService(LanguageRepository languageRepository)
   {
       _languageRepository = languageRepository;
   }

   public async Task<Dictionary<string, string>> getLanguages()
   {
       var languages = await _languageRepository.getLanguages();
       Dictionary<string, string> languageList = new Dictionary<string, string>();
       foreach (var languageKey in languages.translation.Keys)
       {
           languageList.Add(languageKey, languages.translation[languageKey].Name);
       }
       return languageList;
   }
}