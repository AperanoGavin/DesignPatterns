namespace ShipFactory.Command;

public interface ICommand
{
    string Execute();
    string? ParseCommandParameters(string[] commandParams);
}