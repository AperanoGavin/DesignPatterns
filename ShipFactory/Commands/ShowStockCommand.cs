namespace ShipFactory
{
    public class ShowStockCommand : ICommand
    {
        private readonly Inventory _inventory;

        public ShowStockCommand(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Execute()
        {
            _inventory.ShowInventory();
        }
    }
}
