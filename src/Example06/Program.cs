bool running = true;
Queue<string?> requestQueue = new();

Thread monitoringThread = new Thread(MonitorQueue);
monitoringThread.Start();

Console.WriteLine("Веб-сервер запущен: ");

while (running)
{
    Console.Write("Запрос: ");
    string request = Console.ReadLine()!;
    if (request?.ToLower() == "q")
    {
        running = false;
        break;
    }
    // ProcessRequest(request);
    requestQueue.Enqueue(request);
}

monitoringThread.Join();

void ProcessRequest(string? request)
{
    Thread.Sleep(3000); // Имитация времени обработки запроса
    Console.WriteLine($"\nОбработан запрос: '{request}'...");
}

void MonitorQueue()
{
    while (running || requestQueue.Count > 0)
    {
        string? request = default;
        if (requestQueue.Count > 0)
        {
            request = requestQueue.Dequeue();
        }

        if (request is not null)
        {
            new Thread(() => ProcessRequest(request)).Start();
        }
        else
        {
            Thread.Sleep(10);
        }
    }
}
