using System;
using System.Collections.Generic;

namespace ShipFactory
{
    public class ProduceShipCommand : ICommand
    {
        private readonly Inventory _inventory;
        private readonly string _shipType;
        private readonly int _quantity;

        public ProduceShipCommand(Inventory inventory, string shipType, int quantity)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _shipType = shipType;
            _quantity = quantity;
        }

        public void Execute()
        {
            if (_inventory.GetShipFactory(_shipType) == null)
            {
                Console.WriteLine($"ERROR: '{_shipType}' is not a recognized spaceship.");
                return;
            }

            var command = new Dictionary<string, int> { { _shipType, _quantity } };
            if (_inventory.IsStockAvailableFor(command))
            {
                _inventory.ProduceShip(_shipType, _quantity);
                Console.WriteLine("STOCK_UPDATED");
            }
            else
            {
                Console.WriteLine("ERROR: Not enough stock available.");
            }
        }
    }
}
