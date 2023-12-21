namespace StatisticsAPI.Services;

public interface IGithubClient
{
    Task<string> GetMainBranch(string user, string repo);

    Task<string> GetCommit(string user, string repo, string branch);

    Task<GithubTree> GetTree(string user, string repo, string sha);

    Task<GithubItem> GetItem(string user, string repo, string sha);
}
