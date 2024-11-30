using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace SetChatFilterCommand.Service;

public unsafe class ChatFilterConfigService
{
    private const int ModifyConfigEvent = 4;
    private const int SaveConfigEvent = 1;
    private const int ChannelBytePosition = 0x134;
    private const int EventKind = 0x10;

    public void SetChatFilterConfig(int tab, int category, int chatType, bool enabled)
    {
        var agentConfigCharacter = AgentModule.Instance()->GetAgentByInternalId(AgentId.ConfigCharacter);
        *((byte*)agentConfigCharacter + ChannelBytePosition) = (byte) tab;

        var returnValue = stackalloc AtkValue[1];
        var values = stackalloc AtkValue[3];

        values[0].SetInt(ModifyConfigEvent);            // Event representing the config modification
        values[1].SetUInt((uint)chatType & 0b01111111); // ID for EventLog to toggle + Mask to limit chatType to 7 bits
        values[2].SetUInt((uint)(enabled ? 1 : 0));     // Should be enabled or disabled
        agentConfigCharacter->ReceiveEvent(returnValue, values, 3, EventKind);

        values[0].SetInt(SaveConfigEvent); // Event representing the config saving
        agentConfigCharacter->ReceiveEvent(returnValue, values, 1, EventKind);
    }
}
