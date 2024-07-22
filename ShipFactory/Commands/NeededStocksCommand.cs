using System;
using System.Collections.Generic;

namespace ShipFactory
{
    public class NeededStocksCommand : ICommand
    {
        private readonly Inventory _inventory;
        private readonly Dictionary<string, int> _shipQuantities;

        public NeededStocksCommand(Inventory inventory, Dictionary<string, int> shipQuantities)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _shipQuantities = shipQuantities ?? throw new ArgumentNullException(nameof(shipQuantities));
           
        }

        public void Execute()
        {
            var totalNeededStock = new Dictionary<string, int>();
            foreach (var kvp in _shipQuantities)
            {
                string shipType = kvp.Key;
                int quantity = kvp.Value;
                var shipParts = _inventory.GetPartsForShip(shipType);

                Console.Write($"{quantity} {shipType} : ");
                var neededStock = new Dictionary<string, int>();

                foreach (var part in shipParts)
                {
                    if (neededStock.ContainsKey(part))
                    {
                        neededStock[part] += quantity;
                    }
                    else
                    {
                        neededStock[part] = quantity;
                    }

                    if (totalNeededStock.ContainsKey(part))
                    {
                        totalNeededStock[part] += quantity;
                    }
                    else
                    {
                        totalNeededStock[part] = quantity;
                    }
                }

                foreach (var part in neededStock)
                {
                    Console.Write($"{part.Value} {part.Key} ");
                }
                Console.WriteLine();
            }

            Console.Write("Total : ");
            foreach (var part in totalNeededStock)
            {
                Console.Write($"{part.Value} {part.Key} ");
            }
            Console.WriteLine();
        }
    }
}
