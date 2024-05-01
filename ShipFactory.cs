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
            Inventory inventory = new Inventory();

            // Ajouter des pièces à l'inventaire
            inventory.AddToStock("Hull_HE1", 10);
            inventory.AddToStock("Engine_EE1", 5);
            inventory.AddToStock("Wings_WE1", 8);
            inventory.AddToStock("Thruster_TE1", 20);

            // Afficher l'inventaire
            inventory.ShowInventory();

            // Exemple de commande
            Dictionary<string, int> command = new Dictionary<string, int>()
            {
                { "Hull_HE1", 1 },
                { "Engine_EE1", 1 },
                { "Wings_WE1", 1 },
                { "Thruster_TE1", 2 }
            };

            // Vérifier si la commande est réalisable et l'exécuter si possible
            if (inventory.VerifyCommand(command))
            {
                Console.WriteLine("Command can be executed.");
                inventory.ExecuteCommand(command);
                Console.WriteLine("Command executed successfully.");
            }
            else
            {
                Console.WriteLine("Command cannot be executed due to insufficient inventory.");
            }

            // Afficher à nouveau l'inventaire après l'exécution de la commande
            inventory.ShowInventory();
        }
    }
}
