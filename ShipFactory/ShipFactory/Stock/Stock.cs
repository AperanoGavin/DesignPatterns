namespace ShipFactory.Stock;

public class Stock
{
    private Dictionary<string, uint> _inventory = new Dictionary<string, uint>
    {
        { "Explorer", 100 },
        { "Cargo", 100 },
        { "Speeder", 100 },
        { "Hull_HE1", 100 },
        { "Hull_HS1", 100 },
        { "Hull_HC1", 100 },
        { "Engine_EE1", 100 },
        { "Engine_ES1", 100 },
        { "Engine_EC1", 100 },
        { "Wings_WE1", 100 },
        { "Wings_WS1", 100 },
        { "Wings_WC1", 100 },
        { "Thruster_TE1", 100 },
        { "Thruster_TS1", 100 },
        { "Thruster_TC1", 100 },
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

    public bool SetQuantity(string itemName, uint newQuantity)
    {
        if (_inventory.ContainsKey(itemName))
        {
            _inventory.Remove(itemName);
            _inventory[itemName] = newQuantity;
            return true;
        }

        return false;
    }

    public uint? GetAvailableQuantity(string itemName)
    {
        if (_inventory.TryGetValue(itemName, out uint quantity))
        {
            return quantity;
        }

        return null;
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