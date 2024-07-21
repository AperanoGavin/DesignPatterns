// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ShipFactory;
using ShipFactory.Cli;

class Program
{
    static void Main(string[] args)
    {
        Cli cli = Cli.Instance;
        Cli cli2 = Cli.Instance;
        Console.WriteLine(ReferenceEquals(cli, cli2));
        Cli.Instance.RunCli();
    }
}
