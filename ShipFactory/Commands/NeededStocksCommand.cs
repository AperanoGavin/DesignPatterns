using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipFactory
{
    public class NeededStocksCommand : ICommand
    {
        private Inventory inventory;
        private Dictionary<string, int> command;

        public NeededStocksCommand(Inventory inventory, Dictionary<string, int> command)
        {
            this.inventory = inventory;
            this.command = command;
        }

        public void Execute()
        {
            var neededStock = inventory.GetNeededStock(command);
            if (neededStock != null)
            {
                Console.WriteLine("Needed Stock:");
                foreach (var kvp in neededStock)
                {
                    Console.WriteLine($"{kvp.Value} {kvp.Key}");
                }
                Console.WriteLine("Total:");
                foreach (var kvp in neededStock.GroupBy(x => x.Key))
                {
                    Console.WriteLine($"{kvp.Sum(x => x.Value)} {kvp.Key}");
                }
            }
        }
    }
}
