using Microsoft.AspNetCore.Mvc;
using StatisticsAPI.Services;

namespace StatisticsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GithubStatisticsController : ControllerBase
{
    private readonly ILogger<GithubStatisticsController> _logger;
    private readonly IStatisticsService _statisticsService;

    public GithubStatisticsController(ILogger<GithubStatisticsController> logger, IStatisticsService statisticsService)
    {
        _logger = logger;
        _statisticsService = statisticsService;
    }

    [HttpGet(Name = "GetGithubStatistics")]
    public IEnumerable<KeyValuePair<string, long>> Get(string user, string repo, string? token) 
        => _statisticsService.GetStatistics(user, repo, token);
}
