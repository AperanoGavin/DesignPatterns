namespace ShipFactory.Command;

using System.Linq;

public class Verify: ICommand
{
    private ICommand? _otherCommand;
    private string? _otherCommandParameters;

    public string Execute()
    {
        var error = _otherCommand?.ParseCommandParameters(_otherCommandParameters ?? "");

        if (error != null)
        {
            return error;
        }

        return "AVAILABLE";
    }

    public string? ParseCommandParameters(string commandParams)
    {
        if (commandParams.Length == 0)
        {
            return "ERROR Parameters for the VERIFY command cannot be empty";
        }

        var commandAndParams = commandParams.Split(new char[] { ' ' }, 2);
        var otherCommandName = commandAndParams[0];
        ICommand? command = CommandMap.Instance.GetCommand(otherCommandName);

        if (command == null)
        {
            return $"ERROR The command {otherCommandName} does not exist";
        }

        _otherCommand = command;
        _otherCommandParameters = commandAndParams.Length > 1 ? commandAndParams[1] : null;

        return null;
    }
}