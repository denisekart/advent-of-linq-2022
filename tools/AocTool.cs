using System.Net;
using System.Text;

public static class AocTool
{
    public static DirectoryInfo? FindThisGitRepositoryRoot()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (currentDirectory != null
            && currentDirectory.Exists
            && !currentDirectory.EnumerateDirectories(".git", SearchOption.TopDirectoryOnly).Any())
        {
            currentDirectory = currentDirectory.Parent;
        }

        return currentDirectory;
    }

    public static bool FileForDayExists(int day)
    {
        var repositoryRoot = FindThisGitRepositoryRoot();
        var dataRoot = repositoryRoot?.EnumerateDirectories("data").FirstOrDefault();
        if (dataRoot == null)
        {
            return false;
        }
        var file = Path.Combine(dataRoot.FullName, GetFilenameForDay(day));

        return File.Exists(file);
    }

    public static string GetFilenameForDay(int day) => $"day-{day}-data.txt";

    public static void SetSessionInformation(string token)
    {
        var root = FindThisGitRepositoryRoot();
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
        var root = FindThisGitRepositoryRoot();
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
            throw new ArgumentException($"The day {dayOfMonth} is not a valid day. The day was specified {(isDaySpecified ? "manually using the command line option" : "automatically using the current day of month")}");
        }

        if (GetSessionToken() is not string sessionToken)
        {
            throw new ArgumentNullException($"Session token is not set. Please provide a valid token using the 'session --set <token>' command.");
        }

        if (FileForDayExists(dayOfMonth))
        {
            throw new InvalidOperationException($"The data for day {dayOfMonth} (./data/{GetFilenameForDay(dayOfMonth)}) already exists. Delete it if you wish to download the data again.");
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
            result.EnsureSuccessStatusCode(); // error will be thrown and handled here
        }
    }

    public static async Task SaveDataForDay(int day, string data)
    {
        var repositoryRoot = FindThisGitRepositoryRoot();
        var dataRoot = repositoryRoot?.EnumerateDirectories("data").FirstOrDefault();
        if (dataRoot == null)
        {
            dataRoot = Directory.CreateDirectory(Path.Combine(repositoryRoot!.FullName, "data"));
        }
        var file = Path.Combine(dataRoot.FullName, GetFilenameForDay(day));

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
        var testClassesRoot = FindThisGitRepositoryRoot()!.EnumerateFiles("Day0.cs", SearchOption.AllDirectories).FirstOrDefault()?.Directory;
        if (testClassesRoot is null)
        {
            throw new InvalidOperationException("There was an error locating the test class directory. I swear there was a 'Day0.cs' file somewhere around here.");
        }

        var filename = $"Day{dayOfMonth}.cs";

        if (testClassesRoot.EnumerateFiles(filename, SearchOption.TopDirectoryOnly).FirstOrDefault() is not null)
        {
            throw new InvalidOperationException($"The test class named {filename} already exists. I shall not destroy data! Abort, abort!");
        }

        var classContent = GenerateTestClassContent(dayOfMonth);

        await File.WriteAllTextAsync(Path.Combine(testClassesRoot.FullName, filename), classContent);
    }

    public static async Task GenerateBenchmarkClass(int dayOfMonth)
    {
        var testClassesRoot = FindThisGitRepositoryRoot()!.EnumerateFiles("Benchmarks.csproj", SearchOption.AllDirectories).FirstOrDefault()?.Directory;
        if (testClassesRoot is null)
        {
            throw new InvalidOperationException("There was an error locating the benchmark class directory. I swear there was a 'Benchmarks.csproj' file somewhere around here.");
        }

        var filename = $"Day{dayOfMonth}.cs";

        if (testClassesRoot.EnumerateFiles(filename, SearchOption.TopDirectoryOnly).FirstOrDefault() is not null)
        {
            throw new InvalidOperationException($"The benchmark class named {filename} already exists. I shall not destroy data! Abort, abort!");
        }

        var classContent = GenerateBenchmarkClassContent(dayOfMonth);

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
        
        using System.Diagnostics;
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

    public static string GenerateBenchmarkClassContent(int dayOfMonth) => $$"""
        /// This file was generated automatically by a tool.
        /// Really! I'm not joking, it's awesome! Try it out!
        /// dotnet run --project .\tools -- --help
        /// 
        /// Happy coding!
        
        using BenchmarkDotNet.Attributes;

        public class Day{{dayOfMonth}}
        {
            private string data = null!;
            Action<string>
                part1 = null!,
                part2 = null!;

            [GlobalSetup]
            public void GlobalSetup()
            {
                data = DataExtensions.GetDataForDay({{dayOfMonth}}) ?? throw new ArgumentNullException(nameof(data));

                var testClass = new AdventOfCode22Tests.Day{{dayOfMonth}}();
                part1 = testClass.Part1_ShouldReturnValidSolution;
                part2 = testClass.Part2_ShouldReturnValidSolution;
            }

            [Benchmark]
            public void Benchmark_Part1()
            {
                part1(data);
            }

            [Benchmark]
            public void Benchmark_Part2()
            {
                part2(data);
            }
        }
        
        """;
    public static void CleanBenchmarkResults()
    {
        var all = FindThisGitRepositoryRoot()!.EnumerateDirectories("BenchmarkDotNet.Artifacts", SearchOption.AllDirectories).ToList();
        all.ForEach(dir => Directory.Delete(dir.FullName, true));
    }

    public static string? GetSanitizedBenchmarkResults()
    {
        var dir = FindThisGitRepositoryRoot()!.EnumerateDirectories("BenchmarkDotNet.Artifacts", SearchOption.AllDirectories).Single();
        var results = dir
            .GetDirectories()
            .First(d => d.Name == "results")
            .EnumerateFiles("*-github.md", SearchOption.TopDirectoryOnly)
            .OrderBy(x => x.Name)
            .Select(f => new {
                name= f.Name, 
                day=int.TryParse(
                    new string(f.Name.Skip(3).TakeWhile(c => char.IsDigit(c)).ToArray()),
                    out var day) ? day : 0, 
                content=File.ReadAllText(f.FullName) })
            .ToList();

        StringBuilder sb = new StringBuilder();
        bool isFirst = true;
        foreach (var day in results)
        {
            if (isFirst)
            {
                isFirst = false;
                var env = new string(day.content.TakeWhile(c => c != '|').ToArray());
                sb.AppendLine(env);
                sb.AppendLine();
            }

            sb.AppendLine($"### Results for day {day.day}");
            sb.AppendLine();
            sb.AppendLine(new string(day.content.SkipWhile(x => x != '|').ToArray()));
        }

        return sb.ToString();
    }

    public static string GetUpdatedReadmeContent(string benchmarks)
    {
        var readme = File.ReadAllText(FindThisGitRepositoryRoot().EnumerateFiles("README.md").Single().FullName);
        var before = readme.Split("## Benchmarks")[0];
        var after = readme.Split("<!-- end benchmarks -->")[1];

        return $$"""
            {{before}}
            ## Benchmarks

            {{benchmarks}}
            <!-- end benchmarks -->
            {{after}}
            """;
    }

    public static void UpdateReadmeWithNewContent(string content)
    {
        var readme = FindThisGitRepositoryRoot().EnumerateFiles("README.md").Single();
        File.WriteAllText(readme.FullName, content);
    }
}
