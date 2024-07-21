// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ShipFactory;
using ShipFactory.Cli;

class Program
{
    static void Main(string[] args)
    {
        Cli.Instance.RunCli();
    }
}
