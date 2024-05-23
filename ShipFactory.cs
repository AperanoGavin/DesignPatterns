using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipFactory
{
    class Ship
    {
        public string Name { get; set; }
        public List<string> Parts { get; set; }

        public Ship(string name)
        {
            Name = name;
            Parts = new List<string>();
        }
    }

    class Inventory
    {
        private Dictionary<string, int> stock;

        public Inventory()
        {
            stock = new Dictionary<string, int>();
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

                // Check if ship type is valid
                if (shipType != "Explorer" && shipType != "Speeder" && shipType != "Cargo")
                {
                    Console.WriteLine($"ERROR: '{shipType}' is not a recognized spaceship.");
                    return null;
                }

                var shipParts = GetShipParts(shipType);
                foreach (var part in shipParts)
                {
                    if (stock.ContainsKey(part))
                    {
                        if (neededStock.ContainsKey(part))
                            neededStock[part] += quantity;
                        else
                            neededStock[part] = quantity;
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Part '{part}' is not available in stock.");
                        return null;
                    }
                }
            }

            return neededStock;
        }

        private List<string> GetShipParts(string shipType)
        {
            switch (shipType)
            {
                case "Explorer":
                    return new List<string> { "Hull_HE1", "Engine_EE1", "Wings_WE1", "Thruster_TE1" };
                case "Speeder":
                    return new List<string> { "Hull_HS1", "Engine_ES1", "Wings_WS1", "Thruster_TS1" , "Thruster_TS1"};
                case "Cargo":
                    return new List<string> { "Hull_HC1", "Engine_EC1", "Wings_WC1", "Thruster_TC1" };
                default:
                    return new List<string>();
            }
        }

        public List<string> GetPartsForShip(string shipType)
       {
        switch (shipType)
            {
                case "Explorer":
                    return new List<string> { "Hull_HE1", "Engine_EE1", "Wings_WE1", "Thruster_TE1" };
                case "Speeder":
                    return new List<string> { "Hull_HS1", "Engine_ES1", "Wings_WS1", "Thruster_TS1", "Thruster_TS1" };
                case "Cargo":
                    return new List<string> { "Hull_HC1", "Engine_EC1", "Wings_WC1", "Thruster_TC1" };
                default:
                    return new List<string>();
            }
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

     
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            inventory.AddToStock("Hull_HE1", 10);
            inventory.AddToStock("Engine_EE1", 5);
            inventory.AddToStock("Wings_WE1", 8);
            inventory.AddToStock("Thruster_TE1", 20);
            inventory.AddToStock("Hull_HS1", 5);
            inventory.AddToStock("Engine_ES1", 3);
            inventory.AddToStock("Wings_WS1", 6);
            inventory.AddToStock("Thruster_TS1", 15);
            inventory.AddToStock("Thruster_TS1", 15);
            inventory.AddToStock("Hull_HC1", 8);
            inventory.AddToStock("Engine_EC1", 4);
            inventory.AddToStock("Wings_WC1", 7);
            inventory.AddToStock("Thruster_TC1", 10);

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ShipFactory <command>");
                return;
            }

            switch (args[0])
            {
                case "STOCKS":
                    inventory.ShowInventory();
                    break;
                case "NEEDED_STOCKS":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("ERROR: Please provide a list of ships.");
                        return;
                    }
                    var command = ParseCommand(args.Skip(1).ToArray());
                    if (command != null)
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
                    break;
                case "VERIFY":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("ERROR: Please provide a ship.");
                            return;
                        }
                       // var ship = ParseCommand(args.Skip(1).ToArray());
                        var ship = ParseCommand(args[1].Split(','));
                        if (ship != null)
                        {
                            foreach (var shipTyp in ship.Keys)
                            {
                                var parts = inventory.GetPartsForShip(shipTyp);
                                if (parts.Count == 0) // Si GetPartsForShip retourne une liste vide, le type de vaisseau n'est pas reconnu
                                {
                                    Console.WriteLine("ERROR `" + shipTyp + "` is not a recognized spaceship");
                                    return;
                                }
                            }
                            Console.WriteLine("All spaceship types are recognized");
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Invalid ship.");
                        }
                        break;
                case "INSTRUCTIONS":
                        if (args.Length < 3)
                        {
                            Console.WriteLine("ERROR: Please provide a quantity and a ship type.");
                            return;
                        }
                        var quantity = int.Parse(args[1]);
                        var shipType = args[2];
                        var command_ = new Dictionary<string, int> { { shipType, quantity } };
                        if (inventory.IsStockAvailableFor(command_))
                        {
                            // Vous devez ajouter ici le code pour générer les instructions d'assemblage avec ProduceShip
                            inventory.ProduceShip(shipType, quantity);
                            
                        }
                        else
                        {
                            Console.WriteLine("UNAVAILABLE");
                        }
                        break;

                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }

        static Dictionary<string, int> ParseCommand(string[] args)
        {
            var command = new Dictionary<string, int>();

            foreach (var arg in args)
            {
                var parts = arg.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    Console.WriteLine($"ERROR: Invalid command format '{arg}'.");
                    return null;
                }
                if (!int.TryParse(parts[0], out int quantity))
                {
                    Console.WriteLine($"ERROR: Invalid quantity '{parts[0]}' for ship '{parts[1]}'.");
                    return null;
                }
                command[parts[1]] = quantity;
            }

            return command;
        }
    }
}
