using System;
using System.Collections.Generic;

namespace ShipFactory
{
    // Classe représentant un vaisseau
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

    // Classe représentant l'inventaire des pièces
    class Inventory
    {
        private Dictionary<string, int> stock;

        public Inventory()
        {
            stock = new Dictionary<string, int>();
        }

        // Ajouter des pièces à l'inventaire
        public void AddToStock(string part, int quantity)
        {
            if (stock.ContainsKey(part))
                stock[part] += quantity;
            else
                stock[part] = quantity;
        }

        // Afficher l'inventaire
        public void ShowInventory()
        {
            Console.WriteLine("Inventory:");
            foreach (var item in stock)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        // Vérifier si une commande est réalisable avec le stock actuel
        public bool VerifyCommand(Dictionary<string, int> command)
        {
            foreach (var item in command)
            {
                if (!stock.ContainsKey(item.Key) || stock[item.Key] < item.Value)
                    return false;
            }
            return true;
        }

        // Exécuter une commande en retirant les pièces nécessaires du stock
        public bool ExecuteCommand(Dictionary<string, int> command)
        {
            if (VerifyCommand(command))
            {
                foreach (var item in command)
                {
                    stock[item.Key] -= item.Value;
                }
                return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ShipFactory <command>");
                return;
            }

            Inventory inventory = new Inventory();
            inventory.AddToStock("Hull_HE1", 10);
            inventory.AddToStock("Engine_EE1", 5);
            inventory.AddToStock("Wings_WE1", 8);
            inventory.AddToStock("Thruster_TE1", 20);

            // Parse the command line arguments
            var command = ParseCommand(args);

            if (command == null)
            {
                Console.WriteLine("Invalid command format.");
                return;
            }

            switch (args[0])
            {
                case "show_inventory":
                    inventory.ShowInventory();
                    break;
                case "verify_command":
                    if (inventory.VerifyCommand(command))
                        Console.WriteLine("Command can be executed.");
                    else
                        Console.WriteLine("Command cannot be executed due to insufficient inventory.");
                    break;
                case "execute_command":
                    if (inventory.ExecuteCommand(command))
                        Console.WriteLine("Command executed successfully.");
                    else
                        Console.WriteLine("Command cannot be executed due to insufficient inventory.");
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }

        static Dictionary<string, int> ParseCommand(string[] args)
        {
            if (args.Length < 2 || args.Length % 2 == 1)
                return null;

            Dictionary<string, int> command = new Dictionary<string, int>();

            try
            {
                for (int i = 1; i < args.Length; i += 2)
                {
                    string part = args[i];
                    int quantity = int.Parse(args[i + 1]);
                    command[part] = quantity;
                }
            }
            catch (FormatException)
            {
                return null;
            }

            return command;
        }
    }
}
