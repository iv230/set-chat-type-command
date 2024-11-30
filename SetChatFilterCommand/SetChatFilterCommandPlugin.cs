using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using SetChatFilterCommand.Handlers;

namespace SetChatFilterCommand;

public sealed class SetChatFilterCommandPlugin : IDalamudPlugin
{
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IPluginLog PluginLog { get; private set; } = null!;
    [PluginService] internal static IChatGui ChatGui { get; private set; } = null!;
    [PluginService] internal static IDataManager DataManager { get; private set; } = null!;

    private const string SetCommandName = "/setchatfilter";
    private const string GetCommandName = "/getchatfilter";

    private readonly SetChatFilterCommandHandler setChatFilterCommandHandler = new();
    private readonly GetChatFilterTypesCommandHandler getChatFilterCommandHandler = new();

    public SetChatFilterCommandPlugin()
    {
        PluginLog.Debug("Initializing SetChatTypeCommandPlugin");

        CommandManager.AddHandler(SetCommandName, new CommandInfo(OnSetCommand)
        {
            HelpMessage = "Change chat filter config."
        });
        
        CommandManager.AddHandler(GetCommandName, new CommandInfo(OnGetCommand)
        {
            HelpMessage = "Get all chat filter IDs."
        });
    }

    public void Dispose()
    {
        CommandManager.RemoveHandler(SetCommandName);
    }

    private void OnSetCommand(string command, string args)
    {
        setChatFilterCommandHandler.OnCommand(command, args);
    }

    private void OnGetCommand(string command, string args)
    {
        getChatFilterCommandHandler.OnCommand(command, args);
    }
}
