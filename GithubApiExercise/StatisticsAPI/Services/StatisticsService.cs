using System.Collections.Concurrent;
using static System.Convert;
using static System.Text.Encoding;

namespace StatisticsAPI.Services;

public class StatisticsService: IStatisticsService
{
    private readonly ILogger<StatisticsService> _logger;

    public StatisticsService(ILogger<StatisticsService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<KeyValuePair<string, long>> GetStatistics(string user, string repo, string? token)
    {
        var githubClient = new GithubClient(token);

        var letters = new ConcurrentDictionary<string, long>();

        var branch = githubClient.GetMainBranch(user, repo).Result;
        var sha = githubClient.GetCommit(user, repo, branch).Result;
        var tree = githubClient.GetTree(user, repo, sha).Result;

        foreach (var item in tree.Tree)
        {
            if (item.Type == "blob" && (item.Path.EndsWith(".js") || item.Path.EndsWith(".ts")))
            {
                var blob = githubClient.GetItem(user, repo, item.Sha).Result.Content;

                var partials = UTF8.GetString(FromBase64String(blob))
                    .Where(c => Char.IsLetter(c))
                    .GroupBy(c => c.ToString().ToLower())
                    .Select(c => new { Char = c.Key, Count = c.Count() });

                foreach (var g in partials)
                {
                    letters.AddOrUpdate(g.Char, g.Count, (k, v) => v + g.Count);
                }
            }
        }
        return letters.OrderByDescending(kvp => kvp.Value);
    }
}
