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

   public async Task<Dictionary<string, List<string>>> getLanguages()
   {
       var languages = await _languageRepository.getLanguages();
       Dictionary<string, List<string>> languageList = new Dictionary<string, List<string>>();
       List<string> language = new List<string>();
       List<string> code = new List<string>();
       foreach (var languageKey in languages.translation.Keys)
       {
           language.Add(languages.translation[languageKey].Name);
           code.Add(languageKey);
       }
       languageList.Add("language",language);
       languageList.Add("code", code);
       return languageList;
   }
}