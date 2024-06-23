namespace Asperoda.Entities.Surtidor;

public interface ISurtidor
{
       Guid Id { get; }
       bool IsFree { get; }
       int MaxFillPrice { get; }
       double Price { get; }
       

       bool Free();
       bool Block();
       void Prefix(int max);
       Task<int> FillAsync(CancellationToken cancellationToken);
}
