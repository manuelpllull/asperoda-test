using Asperoda.Entities;

namespace Asperoda.Services;

public interface IPistaService
{
    void FreeSurtidor(Guid id);
    void BlockSurtidor(Guid id);
    void PrefixSurtidor(Guid id, int max);
    Task<int> FillSurtidorAsync(Guid id, CancellationToken cancellationToken);
    IEnumerable<ISurtidor> GetStatus();
}