namespace ShipFactory
{
    public class SpeederFactory : IShipFactory
    {
        public Ship CreateShip()
        {
            var ship = new Ship("Speeder");
            ship.Parts = new List<string> { "Hull_HS1", "Engine_ES1", "Wings_WS1", "Thruster_TS1", "Thruster_TS1" };
            return ship;
        }
    }
}
