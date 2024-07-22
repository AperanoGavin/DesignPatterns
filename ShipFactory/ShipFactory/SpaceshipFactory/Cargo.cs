namespace ShipFactory.SpaceshipFactory;

public class Cargo: SpaceShipFactory
{
    protected override Dictionary<string, uint> NeededMaterial()
    {
        return new Dictionary<string, uint>
        {
            {
                "Hull_HC1", 1
            },
            {
                "Engine_EC1", 1
            },
            {
                "Wings_WC1", 2
            },
            {
                "Thruster_TC1", 1
            }
        };
    }
}