using Kliensoldali2019_CP6OG3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kliensoldali2019_CP6OG3.Services
{
    //Translator object to handle the translations, make the conversion from the name of the language to the code of it.
    public class Translator
    {
        public Dictionary<String, List<String>> languageDictionary;         //for storing all the avaiable language combinations
        String sourcelang;
        String destlang;
        public String sourceLang { get { return this.sourcelang; } set { sourcelang = GetSourceID(value);} }
        public String destLang { get { return this.destlang; } set { destlang = GetTargetID(value);  } }
        private JsonLanguage languages;

        private Requester r = new Requester();

        public Translator()
        {
        }

        public async Task init()
        {
            languageDictionary = new Dictionary<String, List<String>>();
            String uri = "https://od-api.oxforddictionaries.com:443/api/v1/languages";
            languages = await  r.requestJsonLanguage(uri);
            SetLanguagesFromJson();
 
        }

        public async Task<JsonResult> translate(String word)
        {
            String word_id = word.ToLower();
            String uri = "https://od-api.oxforddictionaries.com:443/api/v1/entries/" + sourcelang + "/" + word_id + "/translations=" + destlang;
            return await r.requestJsonResult(uri);
        }

        public async Task<JsonSynonymAntonym> GetSynonymAntonym(String word)
        {
            String word_id = word.ToLower();
            String uri = "https://od-api.oxforddictionaries.com:443/api/v1/entries/en/" + word+"/synonyms;antonyms";
            return await r.RequestJsonSynonymAntonym(uri);
        }

        //filling the dictionary with the posibble language combinations, so no further Http request will be required
        private void SetLanguagesFromJson()
        {
            foreach (LanguageResult lr in languages.results)
            {
                if (lr.sourceLanguage != null && lr.targetLanguage != null)
                {
                    if (languageDictionary.ContainsKey(lr.sourceLanguage.language))
                    {
                        languageDictionary[lr.sourceLanguage.language].Add(lr.targetLanguage.language);
                    }
                    else
                    {
                        languageDictionary.Add(lr.sourceLanguage.language, new List<String>());
                        languageDictionary[lr.sourceLanguage.language].Add(lr.targetLanguage.language);
                    }
                }
            }

        }
        //returns the available source languages
        public String[] GetSourceLanguages()
        {

            List<String> list = new List<String>();
            foreach (KeyValuePair<String, List<String>> kvp in languageDictionary)
            {
                list.Add(kvp.Key);
            }
            String[] array = list.ToArray();
            return array;
        }
        //returns the available target languages for the given source
        public String[] GetTargetLanguages(String source)
        {
            List<String> list = languageDictionary[source];
            return list.ToArray();
        }

        //functions for internal conversion
        private String GetSourceID(String language)
        {
            foreach (LanguageResult lr in languages.results)
            {
                if (lr.sourceLanguage.language == language)
                    return lr.sourceLanguage.id;
            }
            return null;
        }
        private String GetTargetID(String language)
        {
            foreach (LanguageResult lr in languages.results)
            {
                if(lr.targetLanguage != null)
                if (lr.targetLanguage.language == language)
                    return lr.targetLanguage.id;
            }
            return null;
        }


    }
}
