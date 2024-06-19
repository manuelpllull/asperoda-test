using Asperoda.Entities;
using Asperoda.Interfaces;

namespace Asperoda.Repositories;

public class InMemorySurtidorRepository(IEnumerable<ISurtidor> surtidores) : ISurtidorRepository
{
    private readonly IEnumerable<ISurtidor> _surtidores =  surtidores;
    
    public ISurtidor? GetSurtidorById(Guid id)
    {
        var surtidor =  _surtidores.FirstOrDefault(surtidor => surtidor.Id == id);

        return surtidor;
    }
    
    public IEnumerable<ISurtidor> GetAllSurtidors()
    {
        return _surtidores;
    }
}