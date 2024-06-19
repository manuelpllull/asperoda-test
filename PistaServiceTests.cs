using NUnit.Framework;
using Asperoda.Entities;
using Asperoda.Services;
using Asperoda.Repositories;
using System.Linq;
using NUnit.Framework.Legacy;

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
        Assert.That(surtidor.IsFree);
    }
    
    [Test]
    public void FillSurtidor_ShouldReturnPrefixValue()
    {
        // Arrange
        var surtidor = _repository.GetAllSurtidors().FirstOrDefault();
        if (surtidor == null)
        {
            Assert.Fail("No surtidor found in the repository.");
        }

        // Set a random prefix and free the surtidor\
        surtidor.Free();
        surtidor.Prefix(new Random().Next(10, 100));
        
        Console.WriteLine(surtidor.MaxFillPrice);

        var expectedTotal = surtidor.MaxFillPrice;
        var cancellationToken = new CancellationToken();

        // Act
        var actualTotal = surtidor.Fill(cancellationToken);

        // Assert
        ClassicAssert.AreEqual(expectedTotal, (int) actualTotal);
    }
}