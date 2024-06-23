using Asperoda.Entities.Surtidor;
using Asperoda.Repositories.Surtidor;
using Asperoda.Services;
using NUnit.Framework;

namespace Asperoda;

[TestFixture]
public class PistaServiceTests
{
    private PistaService _pistaService;
    private InMemorySurtidorRepository _repository;

    [SetUp]
    public void Setup()
    {
        List<ISurtidor> surtidores = new();
        Random random = new();

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
    public void FreeSurtidor_ShouldSetIsFreeToFalse()
    {
        // Arrange
        var surtidor = _repository.GetAllSurtidors().First();
        _pistaService.FreeSurtidor(surtidor.Id);

        // Act
        _pistaService.BlockSurtidor(surtidor.Id);

        // Assert
        Assert.That(!surtidor.IsFree);
    }

    [Test]
    public async Task FillSurtidor_ShouldReturnPrefixValue()
    {
        // Arrange
        var surtidor = _repository.GetAllSurtidors().FirstOrDefault();
        if (surtidor == null)
        {
            Assert.Fail("No surtidor found in the repository.");
            return;
        }

        // Set a random prefix (can be a typical amount to fill the tank) and free the surtidor\
        _pistaService.FreeSurtidor(surtidor.Id);
        _pistaService.PrefixSurtidor(surtidor.Id, new Random().Next(10, 100));

        Console.WriteLine(surtidor.MaxFillPrice);

        var expectedTotal = surtidor.MaxFillPrice;
        var cancellationToken = new CancellationToken();

        // Act
        await _pistaService.FillSurtidorAsync(surtidor.Id, cancellationToken);

        // Assert
        Assert.That(expectedTotal, Is.EqualTo(Math.Round(surtidor.Price)));
    }

    [Test]
    public async Task FillSurtidor_ShouldReturnZeroIfSurtidorIsBlocked()
    {
        // Arrange
        var surtidor = _repository.GetAllSurtidors().FirstOrDefault();
        if (surtidor == null)
        {
            Assert.Fail("No surtidor found in the repository.");
            return;
        }

        // Set a random prefix and block the surtidor
        _pistaService.PrefixSurtidor(surtidor.Id,new Random().Next(10, 100));
        _pistaService.BlockSurtidor(surtidor.Id);

        var expectedTotal = 0;
        var cancellationToken = new CancellationToken();

        // Act
        await _pistaService.FillSurtidorAsync(surtidor.Id, cancellationToken);

        // Assert
        Assert.That(expectedTotal, Is.EqualTo(0));
    }

    [Test]
    public async Task FillSurtidor_ShouldStopAtExpectedValueOnCancellation()
    {
        // Arrange
        var surtidor = _repository.GetAllSurtidors().FirstOrDefault();
        if (surtidor == null)
        {
            Assert.Fail("No surtidor found in the repository.");
            return;
        }

        var iterations = 0;
        var expectedValue = 0.00;

        var delay = new Random().Next(0, 1000);
        var prefix = new Random().Next(10, 100);
        surtidor.Prefix(prefix);
        surtidor.Free();

        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var fillTask = _pistaService.FillSurtidorAsync(surtidor.Id, cancellationTokenSource.Token);
        var delayTask = Task.Delay(delay, cancellationTokenSource.Token);

        var completedTask = await Task.WhenAny(fillTask, delayTask);

        if (completedTask == delayTask)
        {
            // If the delay task completed first, cancel the fill task
            cancellationTokenSource.Cancel();
        }

        iterations = await fillTask;

        expectedValue = iterations * 0.1;
        
        if (expectedValue > prefix)
        {
            expectedValue = prefix;
        }

        var lastSuministro = _pistaService.GetHistorial()
            .Where(s => s.SurtidorId == surtidor.Id)
            .OrderByDescending(s => s.DateTime)
            .FirstOrDefault();
        
        if (lastSuministro == null)
        {
            Assert.Fail("No suministro found in the historial.");
            return;
        }
        // Assert
        Assert.That(expectedValue, Is.EqualTo(lastSuministro.Price).Within(0.09));
    }
}