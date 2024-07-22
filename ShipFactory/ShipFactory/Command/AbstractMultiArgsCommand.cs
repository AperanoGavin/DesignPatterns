namespace ShipFactory.Command;

using System.Text.RegularExpressions;

public abstract class AbstractMultiArgsCommand
{
    protected (uint, string)[]? _args;

    protected (uint, string)[]? MergeQuantites()
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
    
    public string? ParseCommandParameters(string commandParams)
    {
        string pattern = @"\s+";
        var filteredParams = Regex.Replace(commandParams, pattern, " ");

        string[] args = commandParams.Split(',');

        List<(uint, string)> parsedArgs = new List<(uint, string)>();

        foreach (string arg in args)
        {
            var quantityAndName = arg.Trim().Split(' ');

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