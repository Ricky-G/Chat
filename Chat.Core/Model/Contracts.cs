
namespace Chat.Core.Model;

public record class Search(string Title, string Year, string imdbID, string Type, string Poster);
public record class MovieSearch(List<Search> Search, string totalResults, string Response);
public record Message(string Name, string Body);
//public record class TelemetrySettings(string AppInsights, string QuickPulse);


public class TelemetrySettings
{
    public string AppInsights { get; set; }
    public string QuickPulse { get; set; }
}