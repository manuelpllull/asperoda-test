using Asperoda.Entities;

namespace Asperoda.Interfaces;

public interface ISurtidorRepository
{
    ISurtidor? GetSurtidorById(Guid id);
    IEnumerable<ISurtidor> GetAllSurtidors();
}