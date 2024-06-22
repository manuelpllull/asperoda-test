using Asperoda.Entities;

namespace Asperoda.Repositories.Surtidor;

public interface ISurtidorRepository
{
    ISurtidor? GetSurtidorById(Guid id);
    IEnumerable<ISurtidor> GetAllSurtidors();
}