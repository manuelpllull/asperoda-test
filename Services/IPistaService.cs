using Asperoda.Entities;

namespace Asperoda.Interfaces;

public interface IPista
{
    void FreeSurtidor(Guid id);
    void PrefixSurtidor(Guid id, float max);
    double FillSurtidor(Guid id);
    IEnumerable<ISurtidor> GetStatus();
}