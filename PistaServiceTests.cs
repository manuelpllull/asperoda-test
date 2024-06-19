using NUnit.Framework;
using Asperoda.Entities;
using Asperoda.Services;
using Asperoda.Repositories;
using System.Linq;

[TestFixture]
public class PistaServiceTests
{
    private PistaService _pistaService;
    private InMemorySurtidorRepository _repository;

    [SetUp]
    public void Setup()
    {
        List<ISurtidor> surtidores = new ();
        Random random = new ();

        for (int i = 0; i < 12; i++)
        {
            int maxFillPrice = random.Next(10, 100); // Random value between 10 and 100

            Surtidor surtidor = new(Guid.NewGuid());
            surtidor.Prefix(maxFillPrice);
            surtidores.Add(surtidor);
        }

        _repository = new InMemorySurtidorRepository(surtidores);
        _pistaService = new PistaService(_repository);
    }

    [Test]
    public void FreeSurtidor_ShouldSetIsFreeToTrue()
    {
        // Arrange
        var surtidor = _repository.GetAllSurtidors().First();
        _pistaService.BlockSurtidor(surtidor.Id);

        // Act
        _pistaService.FreeSurtidor(surtidor.Id);

        // Assert
        Assert.IsTrue(surtidor.IsFree);
    }
}