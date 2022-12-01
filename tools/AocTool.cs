using AdventOfCode22Tests;
using System.Net;

public static class AocTool
{
    public static void SetSessionInformation(string token)
    {
        var root = Utilities.FindThisGitRepositoryRoot();
        File.WriteAllText(Path.Combine(root!.FullName, "session"), token);
        Console.WriteLine("Session token set! -- by the way, don't ever commit this information ;) - put the session token location in .gitignore");
    }

    public static void PrintSessionInformation()
    {
        var token = GetSessionToken();
        if (token is null)
        {
            Console.WriteLine("Current session information is not set. Use the 'session --set <token>' command to set the token.");
        }
        else
        {
            Console.WriteLine($"Session token: {token}");
        }
    }

    public static string? GetSessionToken()
    {
        var root = Utilities.FindThisGitRepositoryRoot();
        var sessionFile = root?.GetFiles("session", SearchOption.TopDirectoryOnly).FirstOrDefault();
        if (sessionFile == null || File.ReadAllText(sessionFile.FullName) is not string sessionToken || string.IsNullOrWhiteSpace(sessionToken))
        {
            return null;
        }
        return sessionToken;
    }

    public static async Task LoadDataForDay(string? day)
    {
        ParseDayInformation(day, out var isDaySpecified, out var dayOfMonth, out var isValidDay);

        if (!isValidDay)
        {
            Console.Error.WriteLine($"The day {dayOfMonth} is not a valid day. The day was specified {(isDaySpecified ? "manually using the command line option" : "automatically using the current day of month")}");
            throw new ArgumentException();
        }

        if (GetSessionToken() is not string sessionToken)
        {
            Console.Error.WriteLine($"Session token is not set. Please provide a valid token using the 'session --set <token>' command.");
            throw new ArgumentNullException();
        }

        if (Utilities.FileForDayExists(dayOfMonth))
        {
            Console.Error.WriteLine($"The for day {dayOfMonth} ({Utilities.GetFilenameForDay(dayOfMonth)}) already exists. Delete it if you wish to download the data again.");
            throw new InvalidOperationException();
        }

        await LoadDataFromAOCWebsite(dayOfMonth, sessionToken);
    }

    public static async Task LoadDataFromAOCWebsite(int dayOfMonth, string sessionToken)
    {
        Console.WriteLine($"Loading data for day {dayOfMonth} ...");

        var baseAddress = new Uri("https://adventofcode.com");
        var inputUrl = $"/2022/day/{dayOfMonth}/input";
        Console.WriteLine($"Requesting data from {baseAddress}{inputUrl}");

        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler
        {
            CookieContainer = cookieContainer
        };
        using var client = new HttpClient(handler)
        {
            BaseAddress = baseAddress,
        };
        cookieContainer.Add(baseAddress, new Cookie("session", sessionToken));
        var result = await client.GetAsync(inputUrl);
        if (result.IsSuccessStatusCode)
        {
            var data = await result.Content.ReadAsStringAsync();
            await SaveDataForDay(dayOfMonth, data);
            Console.WriteLine($"File downloaded successfully!");
        }
        else
        {
            Console.Error.WriteLine("There was en error during data transfer. You should consider invalidating your session token.");
            result.EnsureSuccessStatusCode();
        }
    }
    public static async Task SaveDataForDay(int day, string data)
    {
        var repositoryRoot = Utilities.FindThisGitRepositoryRoot();
        var dataRoot = repositoryRoot?.EnumerateDirectories("data").FirstOrDefault();
        if (dataRoot == null)
        {
            dataRoot = Directory.CreateDirectory(Path.Combine(repositoryRoot!.FullName, "data"));
        }
        var file = Path.Combine(dataRoot.FullName, Utilities.GetFilenameForDay(day));

        await File.WriteAllTextAsync(file, data);
    }

    public static void ParseDayInformation(string? day, out bool isDaySpecified, out int dayOfMonth, out bool isValidDay)
    {
        var numbers = !string.IsNullOrWhiteSpace(day)
                ? string.Join("", day.Select(x => char.IsNumber(x) ? x : (char?)null).OfType<char>()) : null;
        isDaySpecified = int.TryParse(numbers, out dayOfMonth);
        if (!isDaySpecified)
        {
            dayOfMonth = DateTime.Today.Day;
        }
        isValidDay = dayOfMonth > 0 && dayOfMonth < 26;
    }

    public static async Task GenerateTestClass(int dayOfMonth)
    {
        // yeah yeah, I'm cheating
        var testClassesRoot = Utilities.FindThisGitRepositoryRoot()!.EnumerateFiles("Day0.cs", SearchOption.AllDirectories).FirstOrDefault()?.Directory;
        if (testClassesRoot is null)
        {
            Console.Error.WriteLine("There was an error locating the test class directory. I swear there was a 'Day0.cs' file somewhere around here.");
            throw new InvalidOperationException();
        }

        var filename = $"Day{dayOfMonth}.cs";

        if (testClassesRoot.EnumerateFiles(filename, SearchOption.TopDirectoryOnly).FirstOrDefault() is not null)
        {
            Console.Error.WriteLine($"The test class named {filename} already exists. I shall not destroy data! Abort, abort!");
            throw new InvalidOperationException();
        }

        var classContent = GenerateTestClassContent(dayOfMonth);

        await File.WriteAllTextAsync(Path.Combine(testClassesRoot.FullName, filename), classContent);
    }

    public static string GenerateTestClassContent(int dayOfMonth)
    {
        var content = $$""""
        /// This file was generated automatically by a tool.
        /// Test data was automatically downloaded from https://adventofcode.com/
        /// Really! I'm not joking, it's awesome! Try it out!
        /// dotnet run --project .\tools -- --help
        /// 
        /// Happy coding!

        namespace AdventOfCode22Tests;

        /// <summary>
        /// The problem: https://adventofcode.com/2022/day/{{dayOfMonth}}
        /// </summary>
        public class Day{{dayOfMonth}}
        {
            [TestCase("""
                <enter test data here>
                """)]
            [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData),new object[] {"day-{{dayOfMonth}}-data.txt"})]
            public void Part1_ShouldReturnValidSolution(string input)
            {
                var solution = 0;

                Console.WriteLine($"Puzzle solution: {solution}");
            }

            [TestCase("""
                <enter test data here>
                """)]
            [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData),new object[] {"day-{{dayOfMonth}}-data.txt"})]
            public void Part2_ShouldReturnValidSolution(string input)
            {
                var solution = 0;
        
                Console.WriteLine($"Puzzle solution: {solution}");
            }
        }
        
        """";

        return content;
    }
}
