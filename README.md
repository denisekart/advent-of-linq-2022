# ⭐️ Advent of ~~Code~~ LINQ 2022 ⭐️

The [adventofcode.com](https://adventofcode.com/) challenge for year 2022.

Let's do this in [LINQ](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/) this year.

> I know, I know - not everything can be solved by LINQ and not every solution is optimal when using LINQ. That being said, LINQ is fun (I promise)!

**Notice:**

All code written for this years' Advent of Code was synthesized by an organic intelligence (classification pending 😆) called MyBrain™. I do not use intelligent coding assistants or other types of tools to come up with these solutions.

## 🎄🎄 Requirements

* `net7.0` is expected. Download the SDK [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).
* that's it - you didn't expect `node` right?

## 🎄🎄 Outline

Problems added as unit test classes in the project `.\src\AdventOfCode22Tests\AdventOfCode22Tests.csproj` (or you can open the `AdventOfLinq.sln` solution file to find the tests).

Problems will be identified by test classes corresponding on the days of the challenge (e.g. `Day1.cs` will represent the 1st of December challenge).

To run the tests, run the `dotnet test` command in the root of the repository.

To run a specific subset of tests, run the following filtered command:

```pwsh
dotnet test --filter SystemCheck
# or
dotnet test --filter Day0
# or combine them
dotnet test --filter "Day0&SystemCheck"
```

... or do your [worst](https://github.com/Microsoft/vstest-docs/blob/main/docs/filter.md).

## 🎄🎄 Other remarks

~~I'll try to commit the results of a particular challenge no less than 24 hours after the next challenge is revealed.~~
I'll commit sooner, everyone is doing it anyway.

For more, follow me ...uhm... here, I guess.

## 🎄🎄 Fun side project

You can also have fun with the tool that lives in this repository called `aoc`. The tool source code is located in the `tools` folder and is installed in this repository as a dotnet tool.

To use the tool, run the `dotnet tool restore` command once. After that, feel free to invoke the tool using the `dotnet aoc` command.

You can of course start from scratch and install the tool where ever you want, just type in the following:

```pwsh
dotnet pack
dotnet tool install --add-source .\tools\nupkg AocTool
```

(but, there's really no need)

See the next sections on what this tool can do.

> Note: If you do not wish to install the tool, commands `dotnet aoc` and `dotnet run --project .\tools --` are completely interchangeable so you can utilize the tool even when it's not installed - it's just a console application (`dotnet run --project .\tools -- --help` === `dotnet aoc --help`).

**Note to self (you're going to forget anyway):** this is how you update the tool locally after developing new feature:

* update version in AocTool.csproj
* run `dotnet pack`
* run `dotnet tool update --add-source .\tools\nupkg AocTool`

## 🎄🎄 Lazy?

Yes! Head over to `tool/AocTool.csproj` (or install it using the hints from previous section) to do the following awesome tasks:

* Download your personalized puzzle inputs automatically
* Create pre-populated test classes for the current day
* Log in to https://adventofcode.com/ ...sort of
* Be lazy

Ok, here's how it works...

> Make sure you have a CLI open and are located in this repository root folder (the one that has .git folder in it)

1. Make sure you're logged in to https://adventofcode.com
2. Grab the session token (F12-developer tools -> application -> cookies -> https://adventofcode.com -> session -> [copy the value])
3. Run `dotnet aoc session --set <token from step 2>` which will cache the token locally. Yay! You hacked the AOC (run). You only need to do this once per....idk...didn't bother to check.
4. Run `dotnet aoc load` to download the puzzle data for the current day (or use the `--day` flag - if you're late or something). Check out the new `data` directory being created in the root of the repo.
5. Run `dotnet aoc generate` to generate a test class for the current day(or use the `--day` ....yada yada...you know). Check out a brand new test class in your tests project.
6. That's the easy part... solve the puzzle!

> Btw, if you're stuck with the tool, just run `dotnet aoc --help` - I commented the sh...snow out of it ;)

## 🎄🎄 Lazy²? - `dotnet aoc makemyday` !

Assuming you are already logged in using the `dotnet aoc session --set <token>`, just type `dotnet aoc makemyday` and enjoy the tool automatically: 

* create a new test class for the current day, 
* download your personal data set required for the current day challenge,
* link your test data to the newly created test class (ready to run),
* create a benchmark suite already linked to the downloaded test data (ready to run)
* provide you with a website link for the current challenge,
* ~~and bring you a cup of freshly brewed coffee.~~ (alpha - donation required LOL)

![](makemyday.gif)








## Benchmarks

``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1098/21H2)
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


```


### Results for day 1

|          Method |     Mean |   Error |  StdDev |
|---------------- |---------:|--------:|--------:|
| Benchmark_Part1 | 394.8 μs | 2.84 μs | 2.37 μs |
| Benchmark_Part2 | 412.6 μs | 7.83 μs | 8.04 μs |

### Results for day 2

|          Method |     Mean |    Error |   StdDev |
|---------------- |---------:|---------:|---------:|
| Benchmark_Part1 | 569.5 μs | 11.25 μs | 17.18 μs |
| Benchmark_Part2 | 598.8 μs | 11.95 μs | 12.78 μs |

### Results for day 3

|          Method |     Mean |    Error |   StdDev |
|---------------- |---------:|---------:|---------:|
| Benchmark_Part1 | 621.5 μs | 10.77 μs |  9.55 μs |
| Benchmark_Part2 | 478.5 μs |  9.56 μs | 11.38 μs |

### Results for day 4

|          Method |     Mean |     Error |    StdDev |
|---------------- |---------:|----------:|----------:|
| Benchmark_Part1 | 2.639 ms | 0.0516 ms | 0.0530 ms |
| Benchmark_Part2 | 2.548 ms | 0.0415 ms | 0.0368 ms |

### Results for day 5

|          Method |     Mean |   Error |  StdDev |
|---------------- |---------:|--------:|--------:|
| Benchmark_Part1 | 355.6 μs | 6.86 μs | 8.42 μs |
| Benchmark_Part2 | 469.2 μs | 4.15 μs | 3.24 μs |

### Results for day 6

|          Method |       Mean |    Error |    StdDev |
|---------------- |-----------:|---------:|----------:|
| Benchmark_Part1 |   451.5 μs |  9.02 μs |  20.73 μs |
| Benchmark_Part2 | 1,491.0 μs | 62.24 μs | 169.33 μs |

### Results for day 7

|          Method |     Mean |     Error |    StdDev |
|---------------- |---------:|----------:|----------:|
| Benchmark_Part1 | 2.189 ms | 0.0434 ms | 0.0989 ms |
| Benchmark_Part2 | 2.870 ms | 0.0571 ms | 0.1433 ms |

### Results for day 8

|          Method |     Mean |   Error |   StdDev |
|---------------- |---------:|--------:|---------:|
| Benchmark_Part1 | 209.2 ms | 5.54 ms | 16.24 ms |
| Benchmark_Part2 | 299.1 ms | 8.81 ms | 25.84 ms |

### Results for day 9

|          Method |     Mean |    Error |   StdDev |
|---------------- |---------:|---------:|---------:|
| Benchmark_Part1 | 12.13 ms | 0.238 ms | 0.679 ms |
| Benchmark_Part2 | 68.11 ms | 2.012 ms | 5.932 ms |

### Results for day 10

|          Method |     Mean |    Error |   StdDev |
|---------------- |---------:|---------:|---------:|
| Benchmark_Part1 | 133.4 μs |  3.49 μs | 10.23 μs |
| Benchmark_Part2 | 753.0 μs | 13.98 μs | 28.57 μs |


<!-- end benchmarks -->








🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄