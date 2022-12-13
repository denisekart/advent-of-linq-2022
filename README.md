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
| Benchmark_Part1 | 387.6 μs | 6.71 μs | 9.19 μs |
| Benchmark_Part2 | 417.4 μs | 8.28 μs | 9.86 μs |

### Results for day 2

|          Method |     Mean |   Error |   StdDev |
|---------------- |---------:|--------:|---------:|
| Benchmark_Part1 | 470.5 μs | 7.37 μs |  6.53 μs |
| Benchmark_Part2 | 473.6 μs | 9.32 μs | 12.76 μs |

### Results for day 3

|          Method |     Mean |    Error |   StdDev |
|---------------- |---------:|---------:|---------:|
| Benchmark_Part1 | 513.4 μs | 10.18 μs | 16.73 μs |
| Benchmark_Part2 | 394.6 μs |  7.56 μs |  8.41 μs |

### Results for day 4

|          Method |     Mean |     Error |    StdDev |
|---------------- |---------:|----------:|----------:|
| Benchmark_Part1 | 2.144 ms | 0.0197 ms | 0.0153 ms |
| Benchmark_Part2 | 2.160 ms | 0.0424 ms | 0.0580 ms |

### Results for day 5

|          Method |     Mean |   Error |  StdDev |
|---------------- |---------:|--------:|--------:|
| Benchmark_Part1 | 279.4 μs | 3.79 μs | 3.36 μs |
| Benchmark_Part2 | 372.4 μs | 7.37 μs | 6.89 μs |

### Results for day 6

|          Method |       Mean |    Error |   StdDev |
|---------------- |-----------:|---------:|---------:|
| Benchmark_Part1 |   374.6 μs |  7.44 μs | 12.22 μs |
| Benchmark_Part2 | 1,241.9 μs | 24.38 μs | 34.97 μs |

### Results for day 7

|          Method |     Mean |     Error |    StdDev |
|---------------- |---------:|----------:|----------:|
| Benchmark_Part1 | 1.769 ms | 0.0353 ms | 0.0407 ms |
| Benchmark_Part2 | 2.259 ms | 0.0376 ms | 0.0351 ms |

### Results for day 8

|          Method |     Mean |   Error |  StdDev |   Median |
|---------------- |---------:|--------:|--------:|---------:|
| Benchmark_Part1 | 188.4 ms | 3.59 ms | 6.37 ms | 186.4 ms |
| Benchmark_Part2 | 253.0 ms | 4.75 ms | 8.69 ms | 248.9 ms |

### Results for day 9

|          Method |     Mean |    Error |   StdDev |
|---------------- |---------:|---------:|---------:|
| Benchmark_Part1 | 10.17 ms | 0.195 ms | 0.173 ms |
| Benchmark_Part2 | 53.15 ms | 0.748 ms | 1.250 ms |

### Results for day 10

|          Method |     Mean |    Error |   StdDev |   Median |
|---------------- |---------:|---------:|---------:|---------:|
| Benchmark_Part1 | 139.8 μs |  3.46 μs | 10.19 μs | 138.2 μs |
| Benchmark_Part2 | 753.9 μs | 16.93 μs | 49.65 μs | 739.0 μs |

### Results for day 11

|          Method |         Mean |       Error |      StdDev |
|---------------- |-------------:|------------:|------------:|
| Benchmark_Part1 |     122.6 μs |     2.44 μs |     4.34 μs |
| Benchmark_Part2 | 173,154.8 μs | 3,451.75 μs | 7,720.32 μs |

### Results for day 12

|          Method |      Mean |     Error |    StdDev |
|---------------- |----------:|----------:|----------:|
| Benchmark_Part1 |  1.063 ms | 0.0210 ms | 0.0352 ms |
| Benchmark_Part2 | 94.940 ms | 1.8862 ms | 3.0459 ms |

### Results for day 13

|          Method |     Mean |     Error |    StdDev |
|---------------- |---------:|----------:|----------:|
| Benchmark_Part1 | 2.726 ms | 0.0537 ms | 0.0596 ms |
| Benchmark_Part2 | 8.582 ms | 0.1691 ms | 0.2257 ms |


<!-- end benchmarks -->











🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄🎄