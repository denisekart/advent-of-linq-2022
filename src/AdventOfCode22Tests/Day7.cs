/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
using System.Xml.Resolvers;

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/7
/// </summary>
public class Day7
{
    public class Cmd
    {
        public Cmd(string Text, bool isCommand = false, bool isDir = false, bool isFile = false, int size = 0, List<Cmd>? children = null, Cmd? parent = null)
        {
            this.Text = Text;
            IsCommand = isCommand;
            IsDir = isDir;
            IsFile = isFile;
            Size = size;
            Children = children;
            Parent = parent;
        }

        public string Text { get; }
        public bool IsCommand { get; }
        public bool IsDir { get; }
        public bool IsFile { get; }
        public int Size { get; set; }
        public List<Cmd> Children { get; } = new();
        public Cmd? Parent { get; set; }
    }
    static int TotalSum(Cmd curr)
    {
        var childDirs = curr.Children.Where(x => x.IsDir);
        if (childDirs.Any())
        {
            curr.Size += childDirs.Select(TotalSum).Sum();
        }

        return curr.Size;
    }

    static IEnumerable<Cmd> FlattenDirs(Cmd root)
    {
        return root.Flatten(i => i.Children.Where(x => x.IsDir));
    }

    [TestCase("""
        $ cd /
        $ ls
        dir a
        14848514 b.txt
        8504156 c.dat
        dir d
        $ cd a
        $ ls
        dir e
        29116 f
        2557 g
        62596 h.lst
        $ cd e
        $ ls
        584 i
        $ cd ..
        $ cd ..
        $ cd d
        $ ls
        4060174 j
        8033020 d.log
        5626152 d.ext
        7214296 k
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-7-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        Stack<Cmd> dirStack = new();
        List<Cmd> allDirs = new();

        input
            .SplitByLines(true, true)
            .Select(line =>
            {
                return line switch
                {
                    _ when line.StartsWith("$") => new Cmd(line.Substring(2), isCommand: true),
                    _ when char.IsNumber(line[0]) => new Cmd(line.Split(" ")[1], isFile: true, size: int.Parse(line.Split(" ")[0])),
                    _ => null // don't care about dir - cd is the one I need
                };
            })
            .OfType<Cmd>()
            .ForEach(line =>
            {
                if (line.IsCommand && line.Text.StartsWith("cd")) // don't care about ls
                {
                    var cmdParam = line.Text.Substring(2).Trim();

                    if (cmdParam == "..")
                    {
                        allDirs.Add(dirStack.Pop());
                    }
                    else
                    {
                        var possibleParent = dirStack.TryPeek(out var p) ? p : null;
                        var item = new Cmd(cmdParam, isDir: true, children: new(), parent: possibleParent);
                        dirStack.Push(item);
                        allDirs.Add(item);
                    }
                    return;
                }
                if (line.IsFile)
                {
                    var currentDir = dirStack.Peek();
                    currentDir.Children.Add(line);
                    currentDir.Size += line.Size;
                    return;
                }
            });

        var distinctDirs = allDirs
            .DistinctBy(x => (x.Parent, x.Text))
            .ToList();
        var lookup = distinctDirs
            .ToLookup(x => x.Parent);
        foreach ( var dir in distinctDirs)
        {
            if(dir is not null)
                dir.Children.AddRange(lookup[dir]);
        }

        var rootDir = distinctDirs.Where(x => x.Text == "/").First();
        var totalSum = TotalSum(rootDir);
        var solution = distinctDirs.Where(x => x.Size <= 100_000).Sum(x => x.Size);

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
        $ cd /
        $ ls
        dir a
        14848514 b.txt
        8504156 c.dat
        dir d
        $ cd a
        $ ls
        dir e
        29116 f
        2557 g
        62596 h.lst
        $ cd e
        $ ls
        584 i
        $ cd ..
        $ cd ..
        $ cd d
        $ ls
        4060174 j
        8033020 d.log
        5626152 d.ext
        7214296 k
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-7-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        const int totalSpace = 70000000;
        const int minNeededSpace = 30000000;

        Stack<Cmd> dirStack = new();
        List<Cmd> allDirs = new();

        input
            .SplitByLines(true, true)
            .Select(line =>
            {
                return line switch
                {
                    _ when line.StartsWith("$") => new Cmd(line.Substring(2), isCommand: true),
                    _ when line.StartsWith("dir") => new Cmd(line.Substring(3).Trim(), isDir: true, children: new()),
                    _ when char.IsNumber(line[0]) => new Cmd(line.Split(" ")[1], isFile: true, size: int.Parse(line.Split(" ")[0])),
                };
            })
            .ForEach(line =>
            {
                if (line.IsCommand && line.Text.StartsWith("cd"))
                {
                    var cmdParam = line.Text.Substring(2).Trim();

                    if (cmdParam == "..")
                        allDirs.Add(dirStack.Pop());
                    else
                    {
                        var item = new Cmd(cmdParam, isDir: true, children: new(), parent: dirStack.TryPeek(out var p) ? p : null);
                        dirStack.Push(item);
                        allDirs.Add(item);
                    }
                    return;
                }
                if (line.IsFile)
                {
                    var currentDir = dirStack.Peek();
                    currentDir.Children.Add(line);
                    currentDir.Size += line.Size;
                    return;
                }
            });

        var distinctDirs = allDirs
            .DistinctBy(x => (x.Parent, x.Text));
        var lookup = distinctDirs
            .ToLookup(x => x.Parent);
        foreach (var dir in distinctDirs)
        {
            if (dir is not null)
                dir.Children.AddRange(lookup[dir]);
        }

        var rootDir = distinctDirs.Where(x => x.Text == "/").Single();
        var totalSum = TotalSum(rootDir);
        var flat = FlattenDirs(rootDir).ToList();

        var currentSpaceOnDisk = totalSpace - totalSum;
        var solution = flat
            .OrderBy(x => x.Size)
            .Select(x => (x, isEnough: currentSpaceOnDisk + x.Size >= minNeededSpace))
            .First(x => x.isEnough)
            .x.Size;

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
