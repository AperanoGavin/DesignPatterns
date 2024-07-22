using System.Security.Cryptography;

namespace ShipFactory.Command;

using System.Text.RegularExpressions;
using System.Linq;

public class NeededStocks: ICommand
{
    private (uint, string)[]? _args;

    private (uint, string)[]? MergeQuantites()
    {
        List< (uint, string)> mergedQuantities = new List<(uint, string)>();

        if (_args == null)
        {
            return null;
        }
        foreach (var (quantity, spaceshipName) in _args)
        {
            int existingSpaceshipIndex = mergedQuantities.FindIndex(value => value.Item2 == spaceshipName);
            if (existingSpaceshipIndex != -1)
            {
                var (q, name) = mergedQuantities[existingSpaceshipIndex];
                mergedQuantities[existingSpaceshipIndex] = (q + quantity, spaceshipName);
            }
            else
            {
                mergedQuantities.Add((quantity, spaceshipName));
            }
        }

        return mergedQuantities.ToArray();
    }
    
    public string Execute()
    {
        if (_args == null)
        {
            return "NeededStocks execution error: need to parse arguments before executing the command";
        }

        var mergedQuantities = MergeQuantites();

        if (mergedQuantities == null)
        {
            return "NeededStocks execution error: mergeQuantities is null";
        }

        string result = "";

        foreach ((uint quantity, string spaceshipName) in mergedQuantities)
        {
            result += spaceshipName switch
            {
                "Explorer" => $@"{quantity} {spaceshipName}:
{quantity} Hull_HE1
{quantity} Engine_EE1
{quantity * 2} Wings_WE1
{quantity} Thruster_TE1
",
                "Speeder" => $@"{quantity} {spaceshipName}:
{quantity} Hull_HS1
{quantity} Engine_ES1
{quantity * 2} Wings_WS1
{quantity * 2} Thruster_TS1
",
                "Cargo" => $@"{quantity} {spaceshipName}:
{quantity} Hull_HC1
{quantity} Engine_EC1
{quantity * 2} Wings_WC1
{quantity} Thruster_TC1
",
                _ => $"ERROR {spaceshipName} is an unknown spaceship type"
            };
        }

        return result;
    }

    public string? ParseCommandParameters(string commandParams)
    {
        string pattern = @"\s+";
        var filteredParams = Regex.Replace(commandParams, pattern, " ");

        Console.WriteLine($"commandParams = {commandParams}");
        string[] args = commandParams.Split(',');
        Console.WriteLine($"split.length = {args.Length}");
        
        List<(uint, string)> parsedArgs = new List<(uint, string)>();

        foreach (string arg in args)
        {
            Console.WriteLine($"arg = {arg}");
            var quantityAndName = arg.Trim().Split(' ');

            // foreach (var q in quantityAndName)
            // {
            //      Console.WriteLine($"Quantity = {q[0]}");
            // }

            if (quantityAndName.Length != 2)
            {
                return "ERROR Quantity and Spaceship name should be provided";
            }

            if (uint.TryParse(quantityAndName[0], out uint quantity))
            {
                parsedArgs.Add((quantity, quantityAndName[1]));
            }
            else
            {
                return "ERROR Quantity should be an unsigned integer";
            }
        }

        _args = parsedArgs.ToArray();
        
        return null;
    }
}