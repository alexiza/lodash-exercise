# lodash-exercise

This web service implements a single endpoint to retrieve the frequency of every letter in .js and .ts files in a given GitHub repository.

The endpoint is implemented in `GithubStatisticsController`, which depends on `StatisticsService` to do the actual computation.

The GitHubAPI calls are abstracted into the `GithubClient` utility class.

The program can call GitHub anonymously, but to do the computation on a reasonably big repository like the one proposed (lodash/lodash) 
without hitting the GitHub rate limits (60 request per hour for non authenticated calls), a GitHub token is needed for authentication.

The service can be demonstrated using the Swagger page included in the project.
