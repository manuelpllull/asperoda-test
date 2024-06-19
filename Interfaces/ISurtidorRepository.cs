namespace Asperoda.Repository;

public interface ISurtidorRepository
{
    void FreeSurtidor(Guid id);
    bool GetSurtidorStatus(Guid id);
    int? GetSurtidorMaxFill(Guid id);
    double FillSurtidor(Guid id, CancellationToken cancellationToken);
}