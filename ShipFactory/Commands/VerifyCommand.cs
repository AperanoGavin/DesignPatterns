namespace ShipFactory
{
    public class VerifyCommand : ICommand
    {
        private readonly Inventory _inventory;
        private readonly Dictionary<string, int> _ships;

        public VerifyCommand(Inventory inventory, Dictionary<string, int> ships)
        {
            _inventory = inventory;
            _ships = ships;
        }

        public void Execute()
        {
            foreach (var shipType in _ships.Keys)
            {
                var parts = _inventory.GetPartsForShip(shipType);
                if (parts.Count == 0)
                {
                    Console.WriteLine($"ERROR: '{shipType}' is not a recognized spaceship.");
                    return;
                }
            }
            Console.WriteLine("All spaceship types are recognized.");
        }
    }
}
