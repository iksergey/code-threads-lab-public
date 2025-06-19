class Ex1ThreadTaskDemo
{
    public static void Run()
    {
        var worker = () =>
        {
            Console.WriteLine($"work... {Thread.CurrentThread.IsThreadPoolThread}");
        };

        Thread thread = new Thread(new ThreadStart(worker));
        thread.Start();

        Task task = new Task(worker);
        task.Start();

        Task t = Task.Run(worker);

    }
}