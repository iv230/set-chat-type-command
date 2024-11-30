using SetChatFilterCommand.Service;

namespace SetChatFilterCommand.Handlers;

public class SetChatFilterCommandHandler : ICommandHandler
{
    private readonly ChatFilterConfigService chatFilterConfigService = new ();

    public void OnCommand(string command, string args)
    {
        var splitArgs = args.Split(' ');

        if (string.IsNullOrWhiteSpace(args))
        {
            SetChatFilterCommandPlugin.ChatGui.Print($"No arguments provided. Usage: {command} <tab> <type> <category> <enabled>");
            return;
        }

        if (splitArgs.Length != 4)
        {
            SetChatFilterCommandPlugin.ChatGui.Print($"Invalid arguments. Usage: {command} <tab> <type> <category> <enabled>");
            return;
        }

        if (!int.TryParse(splitArgs[0], out var tab) || tab < 0 || tab > 3)
        {
            SetChatFilterCommandPlugin.ChatGui.Print("Invalid tab. Must be between 0 and 3.");
            return;
        }

        if (!int.TryParse(splitArgs[1], out var category))
        {
            SetChatFilterCommandPlugin.ChatGui.Print("Invalid category. Must be a number.");
            return;
        }

        if (!int.TryParse(splitArgs[2], out var type))
        {
            SetChatFilterCommandPlugin.ChatGui.Print("Invalid chat type. Must be a number.");
            return;
        }

        if (!bool.TryParse(splitArgs[3], out var enabled))
        {
            SetChatFilterCommandPlugin.ChatGui.Print("Invalid activation. Must be 'true' or 'false'.");
            return;
        }

        chatFilterConfigService.SetChatFilterConfig(tab, category, type, enabled);
    }
}
