namespace ShipFactory.Command;

public class CommandMap
{
    private static CommandMap? _instance;
    private static object _lock = new object();
    private CommandMap()
    {
    }
    
    public static CommandMap Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new CommandMap();
                }
                return _instance;
            }
        }
    }
    
    public ICommand? GetCommand(string commandName)
    {
        return commandName switch
        {
            "STOCKS" => new Stocks(),
            "VERIFY" => new Verify(),
            "NEEDED_STOCKS" => new NeededStocks(),
            _ => null,
        };
    }
}