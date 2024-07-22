namespace ShipFactory.SpaceshipFactory;

using ShipFactory.Stock;

public class Speeder: SpaceShipFactory
{
    protected override Dictionary<string, uint> NeededMaterial()
    {
        return new Dictionary<string, uint>
        {
            {
                "Hull_HE1", 1
            },
            {
                "Engine_EE1", 1
            },
            {
                "Wings_WE1", 2
            },
            {
                "Thruster_TE1", 1
            }
        };
    }
}