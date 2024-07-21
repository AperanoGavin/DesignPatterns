
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
        
        private ICommand? ParseCommand(string line)
        {
            
            string[] commandAndArguments = line.Trim().Split(" ");
            string commandName = commandAndArguments[0];
            ICommand? command = CommandMap.Instance.GetCommand(commandName);

            if (command == null)
            {
                Console.WriteLine("ERROR The command" + commandName + "does not exist");
                return null;
            }

            var args = commandAndArguments.Skip(1).Take(commandAndArguments.Length).ToArray();

            var error = command.ParseCommandParameters(args);
            if (error != null)
            {
                Console.WriteLine(error);
                return null;
            }

            return command;
        }

        public void RunCli()
        {
            string? line;
            ICommand? command;

            do
            {
                line = Console.ReadLine();

                command = ParseCommand(line ?? "");

                if (command == null)
                {
                    continue;
                }

                Console.WriteLine(command.Execute());
            } while (line != "EXIT");
        }
    }
}
