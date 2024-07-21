namespace ShipFactory.Command;

public class Stocks: ICommand
{
    public string Execute()
    {
        return Stock.Stock.Instance.GetAvailableStock();
    }

    public bool ParseCommandParameters(string[] commandParams)
    {
        Console.WriteLine(commandParams.Length == 0);
        return true;
    }
}