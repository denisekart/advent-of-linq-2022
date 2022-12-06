// See https://aka.ms/new-console-template for more information
public class DataExtensions
{
    public static string? GetDataForDay(int day)
    {
        var repositoryRoot = FindThisGitRepositoryRoot();
        var dataRoot = repositoryRoot?.EnumerateDirectories("data").FirstOrDefault();
        if (dataRoot == null)
        {
            return null;
        }
        var file = Path.Combine(dataRoot.FullName, GetFilenameForDay(day));

        return File.ReadAllText(file);
    }

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

    public static string GetFilenameForDay(int day) => $"day-{day}-data.txt";
}