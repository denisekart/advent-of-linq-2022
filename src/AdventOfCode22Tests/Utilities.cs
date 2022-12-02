namespace AdventOfCode22Tests;

public static class Utilities
{
    /// <summary>
    /// This is just a wrapper around <see cref="LoadData(string)"/> - for test sources
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static IEnumerable<string> LoadTestData(string filename) => new[] {LoadData(filename)};
    
    /// <summary>
    /// Loads the data from the ./data folder using the filename
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    public static string LoadData(string filename)
    {
        var repositoryRoot = FindThisGitRepositoryRoot();
        var dataRoot = repositoryRoot?.EnumerateDirectories("data").FirstOrDefault();
        if(dataRoot == null)
        {
            throw new DirectoryNotFoundException($"The 'data' directory at the root of the repository does not exist. Create one, and make sure the file '{filename}' exists.");
        }

        var file = dataRoot.GetFiles(filename, SearchOption.TopDirectoryOnly).FirstOrDefault();
        if (file == null)
        {
            throw new FileNotFoundException($"The file named '{filename}' does not exist in the 'data' directory under the root of the repository. Make sure you create one.");
        }

        return File.ReadAllText(file.FullName);
    }

    public static DirectoryInfo? FindThisGitRepositoryRoot()
    {
        var currentDirectory = Directory.GetParent(TestContext.CurrentContext.TestDirectory);
        while(currentDirectory != null 
            && currentDirectory.Exists 
            && !currentDirectory.EnumerateDirectories(".git", SearchOption.TopDirectoryOnly).Any())
        {
            currentDirectory = currentDirectory.Parent;
        }

        return currentDirectory;
    }
}