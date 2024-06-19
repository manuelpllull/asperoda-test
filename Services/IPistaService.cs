using Asperoda.Entities;

namespace Asperoda.Services;

public interface IPistaService
{
    void FreeSurtidor(Guid id);
    void BlockSurtidor(Guid id);
    void PrefixSurtidor(Guid id, int max);
    double FillSurtidor(Guid id, CancellationToken cancellationToken);
    IEnumerable<ISurtidor> GetStatus();
}