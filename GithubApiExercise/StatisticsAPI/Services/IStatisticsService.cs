namespace StatisticsAPI.Services;

public interface IStatisticsService
{
    IEnumerable<KeyValuePair<string, long>> GetStatistics(string user, string repo, string? token);
}