using System;
using System.Linq;
using Data.Domain;

namespace Data
{
    public class DbInitializer
    {
        public static void Initialize(EFContext context)
        {
            // context.Database.EnsureCreated();

            // if (context.Languages.Any())
            // {
            //     return;
            // }

            // var currentDate = DateTime.UtcNow;

            // var languages = new Language[]
            // {
            //     new Language{ Name = "Polish", CreatedDate = currentDate, ModifiedDate = currentDate  },
            //     new Language{ Name = "English", CreatedDate = currentDate, ModifiedDate = currentDate  },
            // };

            // foreach (var language in languages)
            // {
            //     context.Languages.Add(language);
            // }

            // context.SaveChanges();
        }
    }
}