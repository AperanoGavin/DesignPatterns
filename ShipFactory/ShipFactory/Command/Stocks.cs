using System.Net.Mail;

namespace ShipFactory.Command;

public class Stocks: ICommand
{
    public string Execute()
    {
        return Stock.Stock.Instance.GetAvailableStock();
    }

    public string? ParseCommandParameters(string commandParams)
    {
        if (commandParams.Length != 0)
        {
            return "STOCKS does not take input parameters";
        }

        return null;
    }
}