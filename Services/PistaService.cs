using Asperoda.Entities;
using Asperoda.Interfaces;

namespace Asperoda.Services;

public class PistaService(ISurtidorRepository repository) : IPistaService
{
    private readonly ISurtidorRepository _repository = repository;
     
    public void FreeSurtidor(Guid id)
    {
        var surtidor = _repository.GetSurtidorById(id);
        surtidor?.Free();
    }

    public void BlockSurtidor(Guid id)
    {
        var surtidor = _repository.GetSurtidorById(id);
        surtidor?.Block();
    }

    public void PrefixSurtidor(Guid id, int max)
    {
        var surtidor = _repository.GetSurtidorById(id);
        surtidor?.Prefix(max);
    }

    public double FillSurtidor(Guid id, CancellationToken cancellationToken)
    {
        var surtidor = _repository.GetSurtidorById(id);
        return surtidor?.Fill(cancellationToken) ?? -1;
    }

    public IEnumerable<ISurtidor> GetStatus()
    {
        return _repository.GetAllSurtidors();
    }
}