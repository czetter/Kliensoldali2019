using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kliensoldali2019_CP6OG3.Models
{

    public class JsonSynonymAntonym
    {
        public SAMetadata metadata { get; set; }
        public ASResult[] results { get; set; }

        public List<string> GetSynonyms()
        {
            List<string> result = new List<string>();
            if (results[0].lexicalEntries[0].entries[0].senses[0].synonyms != null)
                foreach (Synonym1 s in results[0].lexicalEntries[0].entries[0].senses[0].synonyms)
                {
                    result.Add(s.text);
                }
            return result;
        }

        public List<string> GetAntonyms()
        {
            List<string> result = new List<string>();
            foreach (SASens sens in results[0].lexicalEntries[0].entries[0].senses)
                if (sens.antonyms != null)
                    foreach (Antonym a in sens.antonyms)
                    {
                        result.Add(a.text);
                    }
            return result;
        }
    }

    public class SAMetadata
    {
        public string provider { get; set; }
    }

    public class ASResult
    {
        public string id { get; set; }
        public string language { get; set; }
        public SALexicalentry[] lexicalEntries { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class SALexicalentry
    {
        public SAEntry[] entries { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public string text { get; set; }
    }

    public class SAEntry
    {
        public string homographNumber { get; set; }
        public SASens[] senses { get; set; }
    }

    public class SASens
    {
        public Antonym[] antonyms { get; set; }
        public SAExample[] examples { get; set; }
        public string id { get; set; }
        public string[] registers { get; set; }
        public SASubsens[] subsenses { get; set; }
        public Synonym1[] synonyms { get; set; }
    }

    public class Antonym
    {
        public string id { get; set; }
        public string language { get; set; }
        public string text { get; set; }
    }

    public class SAExample
    {
        public string text { get; set; }
    }

    public class SASubsens
    {
        public string id { get; set; }
        public string[] registers { get; set; }
        public Synonym[] synonyms { get; set; }
        public string[] regions { get; set; }
    }

    public class Synonym
    {
        public string id { get; set; }
        public string language { get; set; }
        public string text { get; set; }
    }

    public class Synonym1
    {
        public string id { get; set; }
        public string language { get; set; }
        public string text { get; set; }
    }

}
