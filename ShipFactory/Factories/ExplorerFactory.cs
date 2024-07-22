namespace ShipFactory
{
    public class ExplorerFactory : IShipFactory
    {
        public Ship CreateShip()
        {
            var ship = new Ship("Explorer");
            ship.Parts = new List<string> { "Hull_HE1", "Engine_EE1", "Wings_WE1", "Thruster_TE1" };
            return ship;
        }
    }
}
