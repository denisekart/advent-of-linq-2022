using System.CommandLine;
using static AocTool;

var getSessionOption = new Option<bool?>(new[] { "--get", "-g" }, "Gets the current session information (reads the 'session' file in the repository root)");
var setSessionOption = new Option<string?>(new[] { "--set", "-s" }, "Sets the current session token. Session data can be obtained by copying the 'session' cookie value from the https://adventofcode.com/ site. Make sure you're logged in!");
var sessionCommand = new Command("session", "Gets or sets the authorization session used for downloading your personalized data from https://adventofcode.com/");
sessionCommand.AddOption(getSessionOption);
sessionCommand.AddOption(setSessionOption);
sessionCommand.SetHandler((getSession, setSession) =>
{
    Action action;
    if (getSession == true)
    {
        action = () => PrintSessionInformation();
    }
    else if (!string.IsNullOrWhiteSpace(setSession))
    {
        action = () => SetSessionInformation(setSession);
    }
    else
    {
        action = () => ConsoleWrappers.FailAndExit("Invalid input. Provide a --get or --set <token> option");
    }
    action.AndHandleErrors(successMessage: "Wow, you're a hacker now!");
}, getSessionOption, setSessionOption);

var dayOption = new Option<string?>(new[] { "--day", "-d" }, "Specify the day of month (e.g. 1, day1) or leave blank for current day.");
var loadCommand = new Command("load", "Loads the data for the specified day (or today if the day was not specified).");
loadCommand.AddOption(dayOption);
loadCommand.SetHandler(async (string? day) =>
{
    await LoadDataForDay(day).AndHandleErrors(successMessage: "Your data awaits m' lord!");

}, dayOption);

var generateCommand = new Command("generate", "Generates the test class and the cases fot the specified day.");
generateCommand.AddAlias("gen");
generateCommand.AddOption(dayOption);
generateCommand.SetHandler(async day =>
{
    ParseDayInformation(day, out var _, out var dayOfMonth, out var isValidDay);
    if (!isValidDay)
    {
        ConsoleWrappers.FailAndExit($"The provided day is not valid. Expected a day between 1 and 25, got {dayOfMonth}. (Fun fact, you can only implicitly determine the day between 1st and 25th in a month)");
    }
    await GenerateTestClass(dayOfMonth).AndHandleErrors(successMessage: "Now start coding ya' thin-leaved deciduous hardwood tree of the genus Betula!");
}, dayOption);

var makeMyDayCommand = new Command("makemyday", "Invoked generate and load commands and brings some christmas cheer");
makeMyDayCommand.AddAlias("d");
makeMyDayCommand.AddOption(dayOption);
makeMyDayCommand.SetHandler(async day =>
{
    ParseDayInformation(day, out var isDaySpecified, out var dayOfMonth, out var isValidDay);
    if (!isValidDay)
    {
        ConsoleWrappers.FailAndExit($"The provided day is not valid. Expected a day between 1 and 25, got {dayOfMonth}. (Fun fact, you can only implicitly determine the day between 1st and 25th in a month)");
    }

    await LoadDataForDay(day).AndTreatErrorsAsWarnings(successMessage: $"Data for day {dayOfMonth} downloaded");
    await GenerateTestClass(dayOfMonth).AndTreatErrorsAsWarnings(successMessage: $"Test class for day {dayOfMonth} generated");
    $"Now go to https://adventofcode.com/2022/day/{dayOfMonth} and start reading!".GoodDeveloper();

    ConsoleWrappers.SucceedAndExit("All is good in the world");
}, dayOption);

var rootCommand = new RootCommand("Advent Of Code tool");
rootCommand.AddCommand(sessionCommand);
rootCommand.AddCommand(loadCommand);
rootCommand.AddCommand(generateCommand);
rootCommand.AddCommand(makeMyDayCommand);

await rootCommand.InvokeAsync(args);
