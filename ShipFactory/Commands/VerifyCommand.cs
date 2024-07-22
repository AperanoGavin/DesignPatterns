using System;
using System.Collections.Generic;

namespace ShipFactory
{
    public class VerifyCommand : ICommand
    {
        private readonly Inventory _inventory;
        private readonly Dictionary<string, int> _shipQuantities;

        public VerifyCommand(Inventory inventory, Dictionary<string, int> shipQuantities)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _shipQuantities = shipQuantities ?? throw new ArgumentNullException(nameof(shipQuantities));
        }

        public void Execute()
        {
            foreach (var kvp in _shipQuantities)
            {
                string shipType = kvp.Key;
                if (_inventory.GetShipFactory(shipType) == null)
                {
                    Console.WriteLine($"ERROR: '{shipType}' is not a recognized spaceship.");
                    return;
                }
            }

            if (_inventory.IsStockAvailableFor(_shipQuantities))
            {
                Console.WriteLine("AVAILABLE");
            }
            else
            {
                Console.WriteLine("UNAVAILABLE");
            }
        }
    }
}
