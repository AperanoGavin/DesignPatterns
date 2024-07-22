namespace ShipFactory.Command;

public class Instructions: AbstractMultiArgsCommand, ICommand
{
    public string Execute()
    {
        if (_args == null)
        {
            return "Instructions execution error: need to parse arguments before executing the command";
        }

        var mergedQuantities = MergeQuantites();

        if (mergedQuantities == null)
        {
            return "Instructions execution error: mergeQuantities is null";
        }

        string result = "";

        foreach ((uint quantity, string spaceshipName) in mergedQuantities)
        {
            result += spaceshipName switch
            {
                "Explorer" => $@"PRODUCING {spaceshipName}:
GET_OUT_OF_STOCK 1 Hull_HE1
GET_OUT_OF_STOCK 1 Engine_EE1
GET_OUT_OF_STOCK 1 Wings_WE1
GET_OUT_OF_STOCK 1 Wings_WE1
GET_OUT_OF_STOCK 1 Thruster_TE1
ASSEMBLE Hull_HE1 + Engine_EE1
ASSEMBLE [Hull_HE1, Engine_EE1] + Wings_WE1
ASSEMBLE [Hull_HE1, Engine_EE1, Wings_WE1] + Thruster_TE1
FINISHED {spaceshipName}
",
                "Speeder" => $@"PRODUCING {spaceshipName}:
GET_OUT_OF_STOCK 1 Hull_HS1
GET_OUT_OF_STOCK 1 Engine_ES1
GET_OUT_OF_STOCK 1 Wings_WS1
GET_OUT_OF_STOCK 1 Wings_WS1
GET_OUT_OF_STOCK 1 Thruster_TS1
GET_OUT_OF_STOCK 1 Thruster_TS1
ASSEMBLE Hull_HS1 + Engine_ES1
ASSEMBLE [Hull_HS1, Engine_ES1] + Wings_WS1
ASSEMBLE [Hull_HS1, Engine_ES1, Wings_WS1] + Thruster_TS1
ASSEMBLE [Hull_HS1, Engine_ES1, Wings_WS1, Thruster_TS1] + Thruster_TS1
FINISHED {spaceshipName}
",
                "Cargo" => $@"PRODUCING {spaceshipName}:
GET_OUT_OF_STOCK 1 Hull_HC1
GET_OUT_OF_STOCK 1 Engine_EC1
GET_OUT_OF_STOCK 1 Wings_WC1
GET_OUT_OF_STOCK 1 Wings_WC1
GET_OUT_OF_STOCK 1 Thruster_TC1
ASSEMBLE Hull_HC1 + Engine_EC1
ASSEMBLE [Hull_HC1, Engine_EC1] + Wings_WC1
ASSEMBLE [Hull_HC1, Engine_EC1, Wings_WC1] + Thruster_TC1
FINISHED {spaceshipName}
",
                _ => $"ERROR {spaceshipName} is an unknown spaceship type"
            };
        }

        return result;
    }
}