namespace ShipFactory
{
    public class ProduceShipCommand : ICommand
    {
        private readonly Inventory _inventory;
        private readonly string _shipType;
        private readonly int _quantity;

        public ProduceShipCommand(Inventory inventory, string shipType, int quantity)
        {
            _inventory = inventory;
            _shipType = shipType;
            _quantity = quantity;
        }

        public void Execute()
        {
            if (_inventory.IsStockAvailableFor(new Dictionary<string, int> { { _shipType, _quantity } }))
            {
                _inventory.ProduceShip(_shipType, _quantity);
            }
            else
            {
                Console.WriteLine("UNAVAILABLE");
            }
        }
    }
}
