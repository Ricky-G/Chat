
namespace Chat.Core.Model;

public record Search(string Title, string Year, string ImdbID, string Type, string Poster);
public record MovieSearch(List<Search> Search, string TotalResults, string Response);
public record Fruit(string Source, string Name);
public record struct Message(string Name, string Body);
public record struct TelemetrySettings(string AppInsights, string QuickPulse);
