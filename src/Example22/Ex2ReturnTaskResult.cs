public class Ex2ReturnTaskResult
{
    public static void Run()
    {
        string time = "String.Empty";

        var worker = () =>
        {
            Thread.Sleep(2000);
            // time = $"{DateTime.Now:HH:mm:ss}";
            return $"{DateTime.Now:HH:mm:ss}";
            // Console.WriteLine($"{DateTime.Now:HH:mm:ss}");
        };

        Console.WriteLine($"{DateTime.Now:HH:mm:ss}");
        Task<string> task = new Task<string>(worker);
        // Task task = new Task(worker);
        task.Start();
        // task.Wait();
        // Task.WaitAll(task);

        Console.WriteLine(time);
        Console.WriteLine(task.Result);

    }
}