using System;
using System.Collections.Generic;

namespace ShipFactory
{
    public class VerifyCommand : ICommand
    {
        private Inventory inventory;
        private Dictionary<string, int> command;

        public VerifyCommand(Inventory inventory, Dictionary<string, int> command)
        {
            this.inventory = inventory;
            this.command = command;
        }

        public void Execute()
        {
            foreach (var shipType in command.Keys)
            {
                var parts = inventory.GetShipParts(shipType);
                if (parts.Count == 0)
                {
                    Console.WriteLine($"ERROR `{shipType}` is not a recognized spaceship");
                    return;
                }
            }
            Console.WriteLine("All spaceship types are recognized");
        }
    }
}
