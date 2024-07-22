namespace ShipFactory
{
    public class NeededStocksCommand : ICommand
    {
        private readonly Inventory _inventory;
        private readonly Dictionary<string, int> _ships;

        public NeededStocksCommand(Inventory inventory, Dictionary<string, int> ships)
        {
            _inventory = inventory;
            _ships = ships;
        }

        public void Execute()
        {
            var neededStock = _inventory.GetNeededStock(_ships);
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
