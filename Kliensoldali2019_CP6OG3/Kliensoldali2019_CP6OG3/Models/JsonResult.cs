using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kliensoldali2019_CP6OG3.Models
{

    public class JsonResult
    {
        public Metadata metadata { get; set; }
        public Result[] results { get; set; }

        public List<string> GetTranslations()
        {
            List<string> translations = new List<string>();

            if (results[0].lexicalEntries[0].entries[0].senses != null)
            {
                foreach (Sens sense in results[0].lexicalEntries[0].entries[0].senses)
                {
                    if(sense.translations != null)
                    foreach (Translation3 t in sense.translations)
                    {
                        translations.Add(t.text);
                    }
                }
            }
            if (translations.Count == 0)
            {
                translations = GetOtherTranslations();
            }
            return translations;
        }

        public List<string> GetOtherTranslations()
        {
            List<string> translations = new List<string>();
            if (results[0].lexicalEntries[0].entries[0].senses[0].subsenses != null)
                foreach (Translation1 t in results[0].lexicalEntries[0].entries[0].senses[0].subsenses[0].translations)
                {
                    translations.Add(t.text);
                    Debug.WriteLine(t.text);
                }
            return translations;
        }

    }

    public class Metadata
    {
        public string provider { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string language { get; set; }
        public Lexicalentry[] lexicalEntries { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class Lexicalentry
    {
        public Entry[] entries { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public string text { get; set; }
    }

    public class Entry
    {
        public Grammaticalfeature[] grammaticalFeatures { get; set; }
        public string homographNumber { get; set; }
        public Sens[] senses { get; set; }
    }

    public class Grammaticalfeature
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Sens
    {
        public string id { get; set; }
        public Subsens[] subsenses { get; set; }
        public Note1[] notes { get; set; }
        public Example1[] examples { get; set; }
        public Translation3[] translations { get; set; }
        public string[] domains { get; set; }
    }

    public class Subsens
    {
        public Example[] examples { get; set; }
        public string id { get; set; }
        public Note[] notes { get; set; }
        public Translation1[] translations { get; set; }
        public string[] domains { get; set; }
    }

    public class Example
    {
        public string text { get; set; }
        public Translation[] translations { get; set; }
    }

    public class Translation
    {
        public string language { get; set; }
        public string text { get; set; }
        public string[] regions { get; set; }
    }

    public class Note
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Translation1
    {
        public string language { get; set; }
        public string text { get; set; }
        public string[] regions { get; set; }
        public string[] registers { get; set; }
    }

    public class Note1
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Example1
    {
        public string text { get; set; }
        public Translation2[] translations { get; set; }
    }

    public class Translation2
    {
        public string language { get; set; }
        public string text { get; set; }
    }

    public class Translation3
    {
        public string language { get; set; }
        public string text { get; set; }
    }


}
