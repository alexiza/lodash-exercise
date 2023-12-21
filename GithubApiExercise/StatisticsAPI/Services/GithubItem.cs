namespace StatisticsAPI.Services;

public record GithubItem(
    string Sha,
    string NodeId,
    long Size,
    string Url,
    string Content,
    string Encoding);
