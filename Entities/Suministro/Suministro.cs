namespace Asperoda.Entities.Suministro;

public class Suministro(Guid surtidorId, double price, int? prefix)
{
    public Guid SurtidorId { get; set; } = surtidorId;
    public double Price { get; set; } = price;
    public int? Prefix { get; set; } = prefix;
    public DateTime DateTime { get; set; } = DateTime.Now;
}