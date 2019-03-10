using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kliensoldali2019_CP6OG3.Models
{
    public class JsonLanguage
    {
        public LanguageMetadata metadata { get; set; }
        public LanguageResult[] results { get; set; }
    }

    public class LanguageMetadata
    {
        public string provider { get; set; }
    }

    public class LanguageResult
    {
        public string region { get; set; }
        public string source { get; set; }
        public Sourcelanguage sourceLanguage { get; set; }
        public string type { get; set; }
        public Targetlanguage targetLanguage { get; set; }
    }

    public class Sourcelanguage
    {
        public string id { get; set; }
        public string language { get; set; }
    }

    public class Targetlanguage
    {
        public string id { get; set; }
        public string language { get; set; }
    }
}
