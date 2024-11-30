namespace SetChatFilterCommand.Handlers;

public interface ICommandHandler
{
    public void OnCommand(string command, string args);
}
