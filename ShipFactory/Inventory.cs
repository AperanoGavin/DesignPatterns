using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipFactory
{
    public class Inventory
    {
        private Dictionary<string, int> stock;
        private static Inventory instance;

        private Inventory()
        {
            stock = new Dictionary<string, int>();
        }

        public static Inventory GetInstance()
        {
            if (instance == null)
            {
                instance = new Inventory();
            }
            return instance;
        }

        public void AddToStock(string part, int quantity)
        {
            if (stock.ContainsKey(part))
                stock[part] += quantity;
            else
                stock[part] = quantity;
        }

        public void ShowInventory()
        {
            Console.WriteLine("Inventory:");
            foreach (var item in stock)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        public bool IsStockAvailableFor(Dictionary<string, int> command)
        {
            var neededStock = GetNeededStock(command);
            if (neededStock == null)
                return false;

            foreach (var kvp in neededStock)
            {
                if (!stock.ContainsKey(kvp.Key) || stock[kvp.Key] < kvp.Value)
                    return false;
            }

            return true;
        }

        public Dictionary<string, int> GetNeededStock(Dictionary<string, int> command)
        {
            var neededStock = new Dictionary<string, int>();

            foreach (var kvp in command)
            {
                string shipType = kvp.Key;
                int quantity = kvp.Value;

                IShipFactory factory = GetShipFactory(shipType);
                if (factory == null)
                {
                    Console.WriteLine($"ERROR: '{shipType}' is not a recognized spaceship.");
                    return null;
                }

                var shipParts = factory.CreateShip().Parts;
                foreach (var part in shipParts)
                {
                    if (neededStock.ContainsKey(part))
                        neededStock[part] += quantity;
                    else
                        neededStock[part] = quantity;
                }
            }

            return neededStock;
        }

        public IShipFactory GetShipFactory(string shipType)
        {
            switch (shipType)
            {
                case "Explorer":
                    return new ExplorerFactory();
                case "Speeder":
                    return new SpeederFactory();
                case "Cargo":
                    return new CargoFactory();
                default:
                    return null;
            }
        }

        public List<string> GetPartsForShip(string shipType)
        {
            var factory = GetShipFactory(shipType);
            return factory?.CreateShip().Parts ?? new List<string>();
        }

        public void ProduceShip(string shipType, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Console.WriteLine($"PRODUCING {shipType}");
                var parts = GetPartsForShip(shipType);
                var groupedParts = parts.GroupBy(p => p).ToDictionary(g => g.Key, g => g.Count());

                foreach (var part in groupedParts)
                {
                    Console.WriteLine($"GET_OUT_STOCK {part.Value} {part.Key}");
                }

                string tmp = "TMP1";
                List<string> currentAssembly = new List<string>();
                foreach (var part in groupedParts)
                {
                    for (int j = 0; j < part.Value; j++)
                    {
                        currentAssembly.Add(part.Key);
                        if (j == part.Value - 1) // Si c'est la dernière pièce de ce type
                        {
                            Console.WriteLine($"ASSEMBLE {tmp} {string.Join(",", currentAssembly)}");
                            currentAssembly = new List<string> { tmp }; // Le nouvel assemblage devient la base pour les prochains assemblages
                            tmp = "TMP" + (int.Parse(tmp.Substring(3)) + 1);
                        }
                    }
                }

                Console.WriteLine($"FINISHED {shipType}");
            }
        }
    }
}
