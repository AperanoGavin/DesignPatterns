using System;

namespace ShipFactory
{
    public class ShowStockCommand : ICommand
    {
        private Inventory inventory;

        public ShowStockCommand(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void Execute()
        {
            inventory.ShowInventory();
        }
    }
}
