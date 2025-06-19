class SemaphoreSlimDemo
{
    static Queue<string?> pendingQueue = new();
    static SemaphoreSlim concurrentLimiter = new(initialCount: 3, maxCount: 3);
    static object objLock = new();

    public static void Run()
    {
        Task.Run(GetInfo);
        Task.Delay(2000).Wait();

        Task queueWatcher = Task.Run(WatchQueue);

        Console.WriteLine("Сервер запущен...");
        while (true)
        {
            var request = Console.ReadLine();
            if (request?.Equals("q", StringComparison.OrdinalIgnoreCase) == true)
            {
                break;
            }

            lock (objLock)
            {
                pendingQueue.Enqueue(request);
            }
        }
        concurrentLimiter.Dispose();
    }
    static void WatchQueue()
    {
        while (true)
        {
            string? request = null;
            lock (objLock)
            {

                if (pendingQueue.Count > 0)
                {
                    request = pendingQueue.Dequeue();
                }
            }

            if (request is not null)
            {
                concurrentLimiter.Wait();
                Task.Run(() => HandleRequest(request));
            }
            Task.Delay(100).Wait();
        }
    }

    static void HandleRequest(string? request)
    {
        try
        {
            Task.Delay(5000).Wait(); // эмуляция обработки
            Console.WriteLine($"Обработан запрос: {request}");
        }
        finally
        {
            int previous = concurrentLimiter.Release();
            int current = concurrentLimiter.CurrentCount;
            string text = String.Format(
                "Task: {0} завершил работу, previous семафора: {1}, current: {2}",
                // Thread.CurrentThread.ManagedThreadId,
                Task.CurrentId,
                previous,
                current);
            Console.WriteLine(text);
        }
    }

    private static void GetInfo()
    {
        while (true)
        {
            string text = String.Format(
                "Task: {0}, current: {1}",
                // Thread.CurrentThread.ManagedThreadId,
                Task.CurrentId,
                concurrentLimiter.CurrentCount);
            Console.WriteLine(text);
            // Thread.Sleep(1000);
            Task.Delay(1000).Wait();
        }
    }
}
