using System.Diagnostics.CodeAnalysis;

public static class ConsoleWrappers
{
    [DoesNotReturn]
    public static void FailAndExit(string message)
    {
        BadFire();
        BadDeveloper(message);
        Environment.Exit(-1);
    }

    [DoesNotReturn]
    public static void SucceedAndExit(string message)
    {
        GoodTree();
        GoodDeveloper(message);
        Environment.Exit(0);
    }

    public static void AndHandleErrors(this Action action, string? successMessage = null)
    {
        try
        {
            action();
            if (!string.IsNullOrWhiteSpace(successMessage))
            {
                GoodDeveloper(successMessage);
                GoodTree();
            }
        }
        catch (Exception e)
        {
            FailAndExit(e.Message);
        }
    }
    
    public static async Task AndHandleErrors(this Task taskToExecute, string? successMessage = null)
    {
        try
        {
            await taskToExecute;
            if (!string.IsNullOrWhiteSpace(successMessage))
            {
                GoodDeveloper(successMessage);
                GoodTree();
            }
        }
        catch (Exception e)
        {
            FailAndExit(e.Message);
        }
    }

    public static async Task AndTreatErrorsAsWarnings(this Task taskToExecute, string? successMessage = null)
    {
        try
        {
            await taskToExecute;
            if (!string.IsNullOrWhiteSpace(successMessage))
            {
                GoodDeveloper(successMessage);
            }
        }
        catch (Exception e)
        {
            SketchyDeveloper(e.Message);
        }
    }

    internal static void BadDeveloper(this string value)
    {
        var fg = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(value);
        Console.ForegroundColor = fg;
    }

    internal static void GoodDeveloper(this string value)
    {
        var fg = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(value);
        Console.ForegroundColor = fg;
    }

    internal static void SketchyDeveloper(this string value)
    {
        var fg = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(value);
        Console.ForegroundColor = fg;
    }

    private static void BadFire() => BadDeveloper(""""

                )
               ) \
              / ) (
              \(_)/ 
        What have you done?!

        """");

    private static void GoodTree() => GoodDeveloper(""""

                _\/_
                 /\
                 /\
                /  \
                /~~\o
               /o   \
              /~~*~~~\
             o/    o \
             /~~~~~~~~\~`
            /__*_______\
                 ||
               \====/
                \__/
        HO HO HO Merry Christmas!

        """");
}