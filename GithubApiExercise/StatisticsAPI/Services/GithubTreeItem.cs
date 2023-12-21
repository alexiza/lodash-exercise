namespace StatisticsAPI.Services;

public record GithubTreeItem(
    string Path,
    string Mode,
    string Type,
    string Sha,
    long Size,
    string Url);
