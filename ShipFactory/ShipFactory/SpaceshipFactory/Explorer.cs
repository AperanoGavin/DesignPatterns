namespace ShipFactory.SpaceshipFactory;

public class Explorer: SpaceShipFactory
{
    protected override Dictionary<string, uint> NeededMaterial()
    {
        return new Dictionary<string, uint>
        {
            {
                "Hull_HS1", 1
            },
            {
                "Engine_ES1", 1
            },
            {
                "Wings_WS1", 2
            },
            {
                "Wings_WS1", 2
            }
        };
    }
}