using System.Security.Cryptography;

namespace ShipFactory.Command;



public class NeededStocks: AbstractMultiArgsCommand, ICommand
{
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
}