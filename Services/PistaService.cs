using Asperoda.Entities;
using Asperoda.Repositories.Surtidor;

namespace Asperoda.Services;

public class PistaService(ISurtidorRepository repository) : IPistaService
{
    private readonly ISurtidorRepository _repository = repository;

    public void FreeSurtidor(Guid id)
    {
        var surtidor = _repository.GetSurtidorById(id);
        
        if (surtidor == null)
        {
            throw new KeyNotFoundException($"Surtidor with id {id} not found.");
        }
        
        surtidor.Free();
    }

    public void BlockSurtidor(Guid id)
    {
        var surtidor = _repository.GetSurtidorById(id);
        
        if (surtidor == null)
        {
            throw new KeyNotFoundException($"Surtidor with id {id} not found.");
        }
        
        surtidor.Block();
    }

    public void PrefixSurtidor(Guid id, int max)
    {
        var surtidor = _repository.GetSurtidorById(id);
        
        if (surtidor == null)
        {
            throw new KeyNotFoundException($"Surtidor with id {id} not found.");
        }
        
        surtidor.Prefix(max);
    }

    public async Task<double> FillSurtidorAsync(Guid id, CancellationToken cancellationToken)
    {
        var surtidor = _repository.GetSurtidorById(id);
        
        if (surtidor == null)
        {
            throw new KeyNotFoundException($"Surtidor with id {id} not found.");
        }
        
        return await surtidor.FillAsync(cancellationToken);
    }

    public IEnumerable<ISurtidor> GetStatus()
    {
        return _repository.GetAllSurtidors();
    }
}