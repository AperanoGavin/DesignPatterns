namespace ShipFactory.SpaceshipFactory;

using ShipFactory.Stock;

public enum ShipType
{
    Speeder,
    Explorer,
    Cargo,
}

public abstract class SpaceShipFactory
{
    protected Stock Stock = Stock.Instance;

    protected abstract Dictionary<string, uint> NeededMaterial(); 

    public static SpaceShipFactory? CreateFactoryFor(ShipType shipType)
    {
        return shipType switch
        {
            ShipType.Speeder => new Speeder(), 
            ShipType.Explorer => new Explorer(),
            ShipType.Cargo => new Cargo(),
            _ => null,
        };
    }
    
    public string Produce(uint spaceshipQuantity)
    {
        string? unavailableItem = null;

        foreach (var (itemName, neededQuantity) in NeededMaterial())
        {
            if (this.Stock.GetAvailableQuantity(itemName) < (neededQuantity * spaceshipQuantity))
            {
                unavailableItem = itemName;
                break;
            }
        }

        if (unavailableItem != null)
        {
            return @$"ERROR Not enough {unavailableItem} in the Stock. 
Available: {this.Stock.GetAvailableQuantity(unavailableItem)}
Needed: {this.NeededMaterial().GetValueOrDefault(unavailableItem)}";
        }
        
        foreach (var (itemName, neededQuantity) in NeededMaterial())
        {
            uint? oldQuantity = this.Stock.GetAvailableQuantity(itemName);

            if (oldQuantity != null)
            {
                uint? quantity = oldQuantity - (neededQuantity * spaceshipQuantity);

                if (quantity != null)
                {
                    this.Stock.SetQuantity(itemName, quantity.Value);
                }
            }
        }

        return "STOCK_UPDATED";
    }
}