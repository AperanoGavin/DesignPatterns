
using System.ComponentModel;
using System.Data;
using System.Linq;


using ShipFactory.Command;

namespace ShipFactory.Cli
{
    public class Cli
    {

        private static Cli? _instance;
        private static object _lock = new object();
        private Cli()
        {
        }

        public static Cli Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Cli();
                    }
                    return _instance;
                }
            }
        }

        private ICommand? GetCommand(string commandName)
        {
            return commandName switch
            {
                "STOCKS" => new Stocks(),
                _ => null,
            };
        }
        
        private (ICommand, string[])? ParseCommand(string line)
        {
            
            string[] commandAndArguments = line.Trim().Split(" ");
            string commandName = commandAndArguments[0];
            ICommand? command = GetCommand(commandName);

            if (command == null)
            {
                Console.WriteLine("ERROR The command" + commandName + "does not exist");
                return null;
            }

            return (command, commandAndArguments.Skip(1).Take(commandAndArguments.Length).ToArray());
        }

        public void RunCli()
        {
            string? line;
            ICommand command;
            string[] args;

            do
            {
                line = Console.ReadLine();

                var commandAndArgs = ParseCommand(line ?? "");

                if (commandAndArgs == null)
                {
                    continue;
                }

                command = commandAndArgs.Value.Item1;
                args = commandAndArgs.Value.Item2;
                command.ParseCommandParameters(args);
                Console.WriteLine(command.Execute());
            } while (line != "EXIT");
        }
    }
}
