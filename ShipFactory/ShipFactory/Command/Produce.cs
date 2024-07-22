using ShipFactory.SpaceshipFactory;

namespace ShipFactory.Command;

public class Produce: AbstractMultiArgsCommand, ICommand
{
    public string Execute()
    {
        if (_args == null)
        {
            return "Instructions execution error: need to parse arguments before executing the command";
        }

        var mergedQuantities = MergeQuantites();

        if (mergedQuantities == null)
        {
            return "Instructions execution error: mergeQuantities is null";
        }

        string result = "";

        foreach ((uint quantity, string spaceshipName) in mergedQuantities)
        {
            string? intermediaryResult;
            if (spaceshipName == "Explorer")
            {
                var explorerFactory = SpaceShipFactory.CreateFactoryFor(ShipType.Explorer);
                intermediaryResult = explorerFactory?.Produce(quantity);
            } else if (spaceshipName == "Speeder")
            {
                var speederFactory = SpaceShipFactory.CreateFactoryFor(ShipType.Speeder);
                intermediaryResult = speederFactory?.Produce(quantity);
            } else if (spaceshipName == "Cargo")
            {
                var cargoFactory = SpaceShipFactory.CreateFactoryFor(ShipType.Cargo);
                intermediaryResult = cargoFactory?.Produce(quantity);
            } else
            {
                intermediaryResult = $"ERROR {spaceshipName} is an unknown spaceship type";
            }

            result += $"{intermediaryResult}\n";
        }

        return result;
    }
    
}