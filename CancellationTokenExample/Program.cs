
var cts = new CancellationTokenSource();

Console.WriteLine("Press 'S' to start the task, 'C' to cancel, or 'Q' to quit.");

while (true)
{
    var key = Console.ReadKey().Key;

    if (key == ConsoleKey.S)
    {
        // Start the task
        Console.WriteLine("\nStarting task...");
        var task = DoWorkAsync(cts.Token);
    }
    else if (key == ConsoleKey.C)
    {
        // Cancel the task
        Console.WriteLine("\nCanceling task...");
        cts.Cancel();
        cts = new CancellationTokenSource(); // Reset for future tasks
    }
    else if (key == ConsoleKey.Q)
    {
        // Quit the program
        break;
    }
}

static async Task DoWorkAsync(CancellationToken cancellationToken)
{
    try
    {
        for (int i = 0; i < 10; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine($"Working... {i}");
            await Task.Delay(500, cancellationToken); // Simulate work
        }
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Task was canceled.");
    }
}