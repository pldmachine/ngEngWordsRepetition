using System;
using Data;
using Data.Domain;

namespace Services.Words
{
    public class WordService : IWordService
    {
        public IRepository<Word> _repository;
        
        public WordService(IRepository<Word> repository)
        {
            _repository = repository;
        }

        public void InsertWord(Word word)
        {
           if (word == null)
           {
               throw new ArgumentNullException(nameof(word));
           }

           _repository.Insert(word);
        }
    }
}