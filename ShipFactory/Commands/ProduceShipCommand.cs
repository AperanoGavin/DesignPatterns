using System;

namespace ShipFactory
{
    public class ProduceShipCommand : ICommand
    {
        private Inventory inventory;
        private string shipType;
        private int quantity;

        public ProduceShipCommand(Inventory inventory, string shipType, int quantity)
        {
            this.inventory = inventory;
            this.shipType = shipType;
            this.quantity = quantity;
        }

        public void Execute()
        {
            var command = new Dictionary<string, int> { { shipType, quantity } };
            if (inventory.IsStockAvailableFor(command))
            {
                inventory.ProduceShip(shipType, quantity);
            }
            else
            {
                Console.WriteLine("UNAVAILABLE");
            }
        }
    }
}
