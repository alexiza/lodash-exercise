namespace StatisticsAPI.Services;

public class GithubClient: IGithubClient
{
    const string BaseUrl = "https://api.github.com/repos/{0}/{1}";
    const string BranchUrl = BaseUrl + "/branches/{2}";
    const string TreeUrl = BaseUrl + "/git/trees/{2}?recursive=1";
    const string ItemUrl = BaseUrl + "/git/blobs/{2}";

    private HttpClient _client;

    public GithubClient(string? token)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client.DefaultRequestHeaders.Add("User-Agent", "TestApp");
        if (!String.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Add("Authorization", "token " + token);
        _client = client;
    }

    public async Task<string> GetMainBranch(string user, string repo)
    {
        var response = await _client.GetAsync(string.Format(BaseUrl, user, repo));

        var result = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<GithubRepo>();

        return result.Default_Branch;
    }

    public async Task<string> GetCommit(string user, string repo, string branch)
    {
        var response = await _client.GetAsync(string.Format(BranchUrl, user, repo, branch));

        var result = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<GithubBranch>();

        return result.Commit.Sha;
    }

    public async Task<GithubTree> GetTree(string user, string repo, string sha)
    {
        var response = await _client.GetAsync(string.Format(TreeUrl, user, repo, sha));

        return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<GithubTree>();
    }

    public async Task<GithubItem> GetItem(string user, string repo, string sha)
    {
        var response = await _client.GetAsync(string.Format(ItemUrl, user, repo, sha));

        return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<GithubItem>();
    }
}
