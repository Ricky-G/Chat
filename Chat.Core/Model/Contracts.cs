
namespace Chat.Core.Model;

public record struct Search(string Title, string Year, string imdbID, string Type, string Poster);
public record struct MovieSearch(List<Search> Search, string totalResults, string Response);

public record struct TelemetrySettings(string AppInsights, string QuickPulse);
public record struct Fruit(string Source, string Name);

//Chat
public record struct Message(string Name, string Body);
