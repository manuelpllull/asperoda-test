using Asperoda.Entities.Suministro;
using Asperoda.Entities.Surtidor;
using Asperoda.Repositories.Surtidor;

namespace Asperoda.Services;

public class PistaService(ISurtidorRepository repository) : IPistaService
{
    private readonly ISurtidorRepository _repository = repository;
    private List<Suministro> _historial = new List<Suministro>();

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

    public async Task<int> FillSurtidorAsync(Guid id, CancellationToken cancellationToken)
    {
        var surtidor = _repository.GetSurtidorById(id);
        
        if (surtidor == null)
        {
            throw new KeyNotFoundException($"Surtidor with id {id} not found.");
        }
        
        var iterations = await surtidor.FillAsync(cancellationToken);
        
        if (cancellationToken.IsCancellationRequested)
        {
            _historial.Add(new Suministro(surtidor.Id, surtidor.Price, surtidor.MaxFillPrice));
            this.FreeSurtidor(surtidor.Id);
            return iterations;
        }
        
        _historial.Add(new Suministro(surtidor.Id, surtidor.Price, surtidor.MaxFillPrice));
        this.FreeSurtidor(surtidor.Id);
        return await surtidor.FillAsync(cancellationToken);
    }

    public IEnumerable<ISurtidor> GetStatus()
    {
        return _repository.GetAllSurtidors();
    }

    public IEnumerable<Suministro> GetHistorial()
    {
        return _historial.OrderBy(s => s.Price).ThenByDescending(s => s.DateTime);    
    }
}