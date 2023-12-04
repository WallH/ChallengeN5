using Confluent.Kafka;
using Domain.Models.DTO;
using KafkaPersistence.Serializers;
using Microsoft.EntityFrameworkCore;
using SqlPersistence;
using SqlPersistence.Abstract;
using SqlPersistence.Persistence;

namespace Test
{
    public class Tests
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder()
              .UseInMemoryDatabase(databaseName: "test")
              .Options;
            var context = new ApplicationContext(options);
            _unitOfWork = new UnitOfWork(context);


        }

        [Test]
        public void TestUnitOfWork()
        {
            _unitOfWork.PermissionTypes.Add(new Domain.Entities.PermissionType { Id = 1, Description = "test" });
            _unitOfWork.Complete();
            var result = _unitOfWork.PermissionTypes.GetAll();
            Assert.AreEqual(result.ToList().Count, 1);
        }

        [Test]
        public void TestSerializer()
        {
            var serializer = new MessageDTOSerializer();
            var messageDTO = new MessageDTO
            {
                Id = Guid.NewGuid(),
                NameOperation = "get"
            };
            var serialized = serializer.Serialize(messageDTO, SerializationContext.Empty);
            Assert.That(serialized, Is.Not.Null);
        }
    }
}