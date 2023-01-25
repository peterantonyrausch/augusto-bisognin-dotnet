using System.Net.Http.Json;
using System.Web;
using Questao2.Dtos;

namespace Questao2;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(string baseUrl)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    /// <summary>
    ///     Get all matche results of the specified team and year.
    /// </summary>
    /// <param name="teamName">The team's name.</param>
    /// <param name="year">The year to fetch.</param>
    /// <returns>A task with the results.</returns>
    /// <exception cref="HttpRequestException">An error ocurred during the request.</exception>
    /// <exception cref="ApplicationException">An error ocurred when reading the response.</exception>
    public async Task<IEnumerable<MatchResult>> GetMatchesByTeamAndYear(string teamName, int year)
    {
        var results = await Task.WhenAll(
            GetAllPages(teamName, MatchFilters.TeamNameLocation.AsTeam1, year),
            GetAllPages(teamName, MatchFilters.TeamNameLocation.AsTeam2, year)
        );

        return results.Aggregate(new List<MatchResult>(), (list, result) => list.Concat(result).ToList());
    }

    private async Task<IEnumerable<MatchResult>> GetAllPages(string teamName,
        MatchFilters.TeamNameLocation nameLocation, int year)
    {
        var filters = new MatchFilters(teamName, nameLocation, year);
        var firstPage = await GetPage(1, filters);

        var requests = new List<Task<ResultPage<MatchResult>>>();
        for (var pageNumber = 2; pageNumber <= firstPage.TotalPages; pageNumber++)
            requests.Add(GetPage(pageNumber, filters));
        var pages = await Task.WhenAll(requests);

        return pages.Aggregate(firstPage.Data, (current, page) => current.Concat(page.Data));
    }

    private async Task<ResultPage<MatchResult>> GetPage(int pageNumber, MatchFilters filters)
    {
        var queryParams = HttpUtility.ParseQueryString(string.Empty);
        queryParams["year"] = filters.Year.ToString();
        var teamParamName = filters.NameLocation == MatchFilters.TeamNameLocation.AsTeam1 ? "team1" : "team2";
        queryParams[teamParamName] = filters.TeamName;
        queryParams["page"] = pageNumber.ToString();

        var response = await _httpClient.GetAsync($"api/football_matches?{queryParams}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ResultPage<MatchResult>>();
        if (result == null)
            throw new ApplicationException("Error reading the response.");

        return result;
    }

    private class MatchFilters
    {
        public string TeamName { get; }
        public TeamNameLocation NameLocation { get; }
        public int Year { get; }

        public MatchFilters(string teamName, TeamNameLocation nameLocation, int year)
        {
            TeamName = teamName;
            NameLocation = nameLocation;
            Year = year;
        }

        internal enum TeamNameLocation
        {
            AsTeam1 = 1,
            AsTeam2 = 2
        }
    }
}