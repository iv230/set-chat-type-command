using System.Collections.Generic;
using System.Linq;
using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace SetChatFilterCommand.Handlers;

public class GetChatFilterTypesCommandHandler : ICommandHandler
{
    private readonly ExcelSheet<LogFilter> logFilterSheet = SetChatFilterCommandPlugin.DataManager.GetExcelSheet<LogFilter>();

    public void OnCommand(string command, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            var categories = logFilterSheet
                             .GroupBy(row => row.Category)
                             .Select(group => group.Key)
                             .OrderBy(category => category)
                             .ToArray();

            SetChatFilterCommandPlugin.ChatGui.Print("All categories:");
            foreach (var category in categories)
            {
                SetChatFilterCommandPlugin.ChatGui.Print($"- Category {category}: {ChatFilters[category]}");
            }
        }
        else if (byte.TryParse(args, out var category))
        {
            var filteredLogFilters = logFilterSheet
                                     .Where(row => row.Category == category)
                                     .OrderBy(row => row.DisplayOrder)
                                     .ToArray();

            if (filteredLogFilters.Length == 0)
            {
                SetChatFilterCommandPlugin.ChatGui.Print($"No type for category {category}.");
                return;
            }

            SetChatFilterCommandPlugin.ChatGui.Print($"All types for category {category} :");
            for (uint i = 0; i < filteredLogFilters.Length; i++)
            {
                SetChatFilterCommandPlugin.ChatGui.Print($"[{i}] {filteredLogFilters[i].Name}");
            }
        }
        else
        {
            SetChatFilterCommandPlugin.ChatGui.Print("Invalid argument. Provide a valid category number. Type without argument to see a list of categories.");
        }
    }

    public Dictionary<int, string> ChatFilters { get; set; } = new()
    {
        { 0, "Unused" },
        { 1, "General discussion" },
        { 2, "Announcement" },
        { 3, "Empty" },
        { 4, "Battle/Self" },
        { 5, "Battle/Party" },
        { 6, "Battle/Alliance" },
        { 7, "Battle/Others" },
        { 8, "Battle/Enemies fought" },
        { 9, "Battle/Other enemies" },
        { 10, "Battle/Friendly NPC" },
        { 11, "Companion/Self" },
        { 12, "Companion/Party" },
        { 13, "Companion/Alliance" },
        { 14, "Companion/Others" },
    };
}
