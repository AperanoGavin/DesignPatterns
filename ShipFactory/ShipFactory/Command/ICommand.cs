namespace ShipFactory.Command;

public interface ICommand
{
    string Execute();
    bool ParseCommandParameters(string[] commandParams);
}