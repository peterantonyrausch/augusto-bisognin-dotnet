namespace Questao2;

public static class Program
{
    private const string BaseUrl = "https://jsonmock.hackerrank.com";

    public static void Main()
    {
        var teamName = "Paris Saint-Germain";
        var year = 2013;
        var totalGoals = GetTotalScoredGoals(teamName, year);

        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");

        teamName = "Chelsea";
        year = 2014;
        totalGoals = GetTotalScoredGoals(teamName, year);

        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");

        // Output expected:
        // Team Paris Saint-Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static int GetTotalScoredGoals(string team, int year)
    {
        var client = new ApiClient(BaseUrl);

        try
        {
            var results = client.GetMatchesByTeamAndYear(team, year).Result;

            return results.Aggregate(0, (totalGoals, result) => result.Team1 == team
                ? totalGoals + int.Parse(result.Team1Goals)
                : totalGoals + int.Parse(result.Team2Goals));
        }
        catch (Exception e) when (e is AggregateException)
        {
            if (e.InnerException is not HttpRequestException or ApplicationException) throw;

            Console.Error.WriteLine($"Error fetching results: {e.InnerException.Message}");
            Environment.Exit(0);
        }

        return 0;
    }
}