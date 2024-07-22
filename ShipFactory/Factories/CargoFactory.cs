namespace ShipFactory
{
    public class CargoFactory : IShipFactory
    {
        public Ship CreateShip()
        {
            var ship = new Ship("Cargo");
            ship.Parts = new List<string> { "Hull_HC1", "Engine_EC1", "Wings_WC1", "Thruster_TC1" };
            return ship;
        }
    }
}
