namespace ShipFactory.Command;

using System.Linq;

public class Verify: ICommand
{
    private ICommand? _otherCommand;
    private string[]? _otherCommandParameters;
    
    
    public string Execute()
    {
        var error = _otherCommand?.ParseCommandParameters(_otherCommandParameters ?? [""]);

        if (error != null)
        {
            return error;
        }

        return "AVAILABLE";
    }

    public string? ParseCommandParameters(string[] commandParams)
    {
        if (commandParams.Length == 0)
        {
            return "ERROR Parameters for the VERIFY command cannot be empty";
        }

        var otherCommandName = commandParams[0];
        ICommand? command = CommandMap.Instance.GetCommand(otherCommandName);

        if (command == null)
        {
            return $"ERROR The command {commandParams} does not exist";
        }

        _otherCommand = command;
        _otherCommandParameters = commandParams.Skip(1).Take(commandParams.Length).ToArray();

        return null;
    }
}