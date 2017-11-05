using System;
using System.Linq;
using Data;
using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace Tests
{
    public class GenericRepositoryTests
    {
        IRepository<MockObject> _repository;
        DbContext _context;

        public GenericRepositoryTests()
        {
            PrepareEmptyDatabase();
        }

        [Fact]
        public void Check_if_new_created_object_incremented()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var obj1 = new MockObject()
            {
                Name = "obj1"
            };

            var obj2 = new MockObject()
            {
                Name = "obj2"
            };

            //Act
            _repository.Insert(obj1);
            _repository.Insert(obj2);

            //Assert
            Assert.True(obj1.Id > 0);
            Assert.True(obj2.Id > 0);
        }

        [Fact]
        public void Check_if_can_add_multiple_entities()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var entities = new MockObject[]
            {
                new MockObject(){ Name = "obj1"},
                new MockObject(){ Name = "obj2"},
                new MockObject(){ Name = "obj3"},
                new MockObject(){ Name = "obj4"}
            };

            //Act
            _repository.Insert(entities);

            //Assert
            var count = _repository.Table.Count();
            Assert.Equal(4, count);
        }

        [Fact]
        public void Check_if_can_list_new_objects()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var obj1 = new MockObject()
            {
                Name = "obj1"
            };

            var obj2 = new MockObject()
            {
                Name = "obj2"
            };

            _repository.Insert(obj1);
            _repository.Insert(obj2);

            var objects = _repository.Table.ToList();

            Assert.Equal(objects.Count, 2);
        }

        [Fact]
        public void Check_if_can_get_entity_by_id()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var createdObj = new MockObject()
            {
                Name = "objA"
            };

            //Act
            _repository.Insert(createdObj);
            var existingObj = _repository.GetById(createdObj.Id);

            //Assert
            Assert.NotNull(existingObj);
            Assert.Equal(createdObj.Name, existingObj.Name);
        }

        [Fact]
        public void Check_if_can_update_entity()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var createdObj = new MockObject() { Name = "objA" };
            _repository.Insert(createdObj);
            var existingObj = _repository.GetById(createdObj.Id);

            //Act

            existingObj.Name = "objB";
            _repository.Update(existingObj);

            var updatedObject = _repository.GetById(existingObj.Id);

            //Assert
            Assert.Equal("objB", updatedObject.Name);
        }

        [Fact]
        public void Check_if_can_update_multiple_entities()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var entities = new MockObject[]
            {
                new MockObject(){ Name = "obj1"},
                new MockObject(){ Name = "obj2"},
                new MockObject(){ Name = "obj3"},
                new MockObject(){ Name = "obj4"}
            };
            _repository.Insert(entities);

            var existingEntities = _repository.Table.ToList();

            //Act
            existingEntities.ForEach(e => e.Name = "obj");
            _repository.Update(existingEntities);

            var count = _repository.Table.Where(e => e.Name == "obj").Count();

            //Assert
            Assert.Equal(4, count);
        }

        [Fact]
        public void Check_if_can_delete_entity()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var createdObj = new MockObject() { Name = "objA" };
            _repository.Insert(createdObj);
            var existingObj = _repository.GetById(createdObj.Id);

            //Act
            _repository.Delete(existingObj);

            var deletedObject = _repository.GetById(existingObj.Id);

            //Assert
            Assert.Null(deletedObject);
        }

           [Fact]
        public void Check_if_can_delete_multiple_entities()
        {
            //Arrange
            _repository = new EFRepository<MockObject>(_context);

            var entities = new MockObject[]
            {
                new MockObject(){ Name = "obj1"},
                new MockObject(){ Name = "obj2"},
                new MockObject(){ Name = "obj3"},
                new MockObject(){ Name = "obj4"}
            };
            _repository.Insert(entities);

            var existingEntities = _repository.Table.ToList();

            //Act
            _repository.Delete(existingEntities);

            var count = _repository.Table.Count();

            //Assert
            Assert.Equal(0, count);
        }

        private void PrepareEmptyDatabase()
        {
            var builder = new DbContextOptionsBuilder<EFContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var options = builder.Options;
            _context = new EFContextProxy(options);
        }

        public class EFContextProxy : EFContext
        {
            public EFContextProxy(DbContextOptions<EFContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<MockObject>().ToTable("MockObject");
                base.OnModelCreating(modelBuilder);
            }
        }

        public class MockObject : BaseEntity
        {
            public string Name { get; set; }
        }
    }
}
