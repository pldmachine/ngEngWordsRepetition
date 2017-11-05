using System;

namespace Data.Domain
{
    public class Word: BaseEntity
    {
        public string Key { get; set; }
        public int LanguageID { get; set; }

        public Language Language { get; set; }
    }
}