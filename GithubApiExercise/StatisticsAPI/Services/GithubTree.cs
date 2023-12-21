namespace StatisticsAPI.Services;

public record GithubTree(
    string Sha,
    string Url,
    List<GithubTreeItem> Tree,
    bool Truncated);
