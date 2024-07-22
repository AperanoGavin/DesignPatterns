using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = Inventory.GetInstance();
            inventory.AddToStock("Hull_HE1", 10);
            inventory.AddToStock("Engine_EE1", 5);
            inventory.AddToStock("Wings_WE1", 8);
            inventory.AddToStock("Thruster_TE1", 20);
            inventory.AddToStock("Hull_HS1", 5);
            inventory.AddToStock("Engine_ES1", 3);
            inventory.AddToStock("Wings_WS1", 6);
            inventory.AddToStock("Thruster_TS1", 15);
            inventory.AddToStock("Hull_HC1", 8);
            inventory.AddToStock("Engine_EC1", 4);
            inventory.AddToStock("Wings_WC1", 7);
            inventory.AddToStock("Thruster_TC1", 10);

            var invoker = new CommandInvoker();

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ShipFactory <command>");
                return;
            }

            switch (args[0])
            {
                case "STOCKS":
                    invoker.SetCommand(new ShowStockCommand(inventory));
                    invoker.ExecuteCommand();
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
                        invoker.SetCommand(new NeededStocksCommand(inventory, command));
                        invoker.ExecuteCommand();
                    }
                    break;

                case "VERIFY":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("ERROR: Please provide a ship.");
                        return;
                    }
                    var ship = ParseCommand(args[1].Split(','));
                    if (ship != null)
                    {
                        invoker.SetCommand(new VerifyCommand(inventory, ship));
                        invoker.ExecuteCommand();
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
                    invoker.SetCommand(new ProduceShipCommand(inventory, shipType, quantity));
                    invoker.ExecuteCommand();
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
