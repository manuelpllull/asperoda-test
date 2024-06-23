namespace Asperoda.Entities.Surtidor;

public class Surtidor : ISurtidor
{
    public Guid Id { get; }
    public bool IsFree { get; private set; }
    public int MaxFillPrice { get; private set; }
    public double Price { get; private set; }

    public Surtidor(Guid id)
    {
        Id = id;
        IsFree = true;
        MaxFillPrice = 0;
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

    public async Task<int> FillAsync(CancellationToken cancellationToken)
    {
        if (!IsFree)
        {
            Price = 0;
            return 0;
        }
        
        IsFree = false;
        var iterationsCounter = 0;
        while (MaxFillPrice != 0 && Price <= MaxFillPrice)
        {
            if(cancellationToken.IsCancellationRequested)
            {
                IsFree = true;
                return iterationsCounter;
            }
            Price += 0.1;
            iterationsCounter++;
            await Task.Delay(1);
        }
        
        IsFree = true;
        return iterationsCounter;
    }
}