namespace ShipFactory.Stock;

public class Stock
{
    private Dictionary<string, int> _inventory = new Dictionary<string, int>
    {
        { "Explorer", 1 },
        { "Cargo", 1 },
        { "Speeder", 1 },
        {"Hull_HE1", 1},
        {"Hull_HS1", 1},
        {"Hull_HC1", 1},
        {"Engine_EE1", 1},
        {"Engine_ES1", 1},
        {"Engine_EC1", 1},
        {"Wings_WE1", 1},
        {"Wings_WS1", 1},
        {"Wings_WC1", 1},
        {"Thruster_TE1", 1},
        {"Thruster_TS1", 1},
        {"Thruster_TC1", 1},
    };
    

    private static Stock? _instance;
    private static object _lock = new object();

    private Stock()
    {
    }

    public static Stock Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Stock();
                }
                return _instance;
            }
        }
    }

    public string GetAvailableStock()
    {
        string result = "";
        foreach (var (item, quantity) in _inventory)
        {
            result += $"{quantity} {item}\n";
        }

        return result.TrimEnd();
    }
}