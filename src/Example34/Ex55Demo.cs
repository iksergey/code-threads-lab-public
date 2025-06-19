using System.Collections.Concurrent;

class Ex55Demo
{
    public static void Run()
    {
        var requestQueue = new BlockingCollection<string>(
            new ConcurrentStack<string>());

        Task producer = Task.Run(() =>
        {
            string[] commands = { "SAVE", "DELETE", "UPDATE", "LOAD", "BACKUP" };

            foreach (string command in commands)
            {
                Console.WriteLine($"[Producer] Добавляем команду: {command}");
                requestQueue.Add(command);
                Thread.Sleep(1000);
            }

            Console.WriteLine("[Producer] Завершаем добавление команд");
            requestQueue.CompleteAdding();
        });

        Task consumer = Task.Run(() =>
        {
            Console.WriteLine("[Consumer] Начинаем обработку команд...");

            foreach (string command in requestQueue.GetConsumingEnumerable())
            {
                Console.WriteLine($"[Consumer] Обрабатываем: {command} (LIFO порядок)");
                Thread.Sleep(500);
            }

            Console.WriteLine("[Consumer] Все команды обработаны!");
        });

        Task.WaitAll(producer, consumer);

        Console.WriteLine($"IsCompleted: {requestQueue.IsCompleted}");
        Console.WriteLine($"IsAddingCompleted: {requestQueue.IsAddingCompleted}");
    }
}
