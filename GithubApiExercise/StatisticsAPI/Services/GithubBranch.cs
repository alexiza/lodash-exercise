namespace StatisticsAPI.Services;

public record GithubCommit (string Sha);

public record GithubBranch (GithubCommit Commit);

