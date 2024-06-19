namespace Asperoda.Entities;

public class Surtidor : ISurtidor
{
    public Guid Id { get; }
    public bool IsFree { get; private set; }
    public int? MaxFillPrice { get; private set; }
    public double Price { get; private set; }

    public Surtidor(Guid id)
    {
        Id = id;
        IsFree = true;
        MaxFillPrice = null;
        Price = 0;
    }
    
    public bool Free()
    {
        IsFree = true;
        return IsFree;
    }


    public bool Block()
    {
        IsFree = false;
        return IsFree;
    }

    public void Prefix(int max)
    {
        MaxFillPrice = max;
    }

    public double Fill(CancellationToken cancellationToken)
    {
        if (!IsFree)
        {
            return Price;
        }
        
        IsFree = false;

        while (!cancellationToken.IsCancellationRequested && MaxFillPrice.HasValue && Price <= MaxFillPrice.Value)
        {
            Price += 0.1;
            Thread.Sleep(1);
        }
        
        IsFree = true;
        return Price;
    }
}