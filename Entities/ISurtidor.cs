namespace Asperoda.Entities;

public interface ISurtidor
{
       Guid Id { get; }
       bool IsFree { get; }
       int? MaxFillPrice { get; }
       double Price { get; }
       

       bool Free();
       bool Block();
       void Prefix(int max);
       double Fill(CancellationToken cancellationToken);
}
