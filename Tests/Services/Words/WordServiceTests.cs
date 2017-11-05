using System;
using System.Linq;
using Data;
using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Services.Words;

namespace Tests.Services.Words
{
    public class WordServiceTests
    {
        IWordService _service;
        public WordServiceTests()
        {
            var builder = new DbContextOptionsBuilder<EFContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var options = builder.Options;
            var context = new EFContext(options);
            var repository = new EFRepository<Word>(context);

            _service = new WordService(repository);
        }

        [Fact]
        public void Check_if_can_insert_new_word()
        {
            //Arrange

            var word = new Word()
            {
                Key="Home"
            };

            //Act
            _service.InsertWord(word);

            //Assert
            Assert.True(word.Id>0);
        }

    }

}