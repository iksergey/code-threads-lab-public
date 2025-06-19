using System.Collections.Concurrent;

class Ex23ProducerConsumerExample
{
    public static void Run()
    {
        var queue = new ConcurrentQueue<string>();
        var cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;

        const int producerCount = 3;
        const int consumerCount = 2;
        const int itemsPerProducer = 5;

        var tasks = new List<Task>();

        // Создаем несколько Producer потоков
        for (int producerId = 0; producerId < producerCount; producerId++)
        {
            int id = producerId; // Захватываем переменную для замыкания

            var producerTask = Task.Run(() =>
            {
                for (int i = 0; i < itemsPerProducer; i++)
                {
                    if (token.IsCancellationRequested) break;

                    string item = $"Producer{id}_Item{i}";
                    queue.Enqueue(item);
                    Console.WriteLine($"[Producer {id}] Enqueued: {item} [Thread: {Thread.CurrentThread.ManagedThreadId}]");
                    Thread.Sleep(Random.Shared.Next(50, 200)); // Случайная задержка
                }
                Console.WriteLine($"[Producer {id}] FINISHED");
            }, token);

            tasks.Add(producerTask);
        }

        // Создаем несколько Consumer потоков
        for (int consumerId = 0; consumerId < consumerCount; consumerId++)
        {
            // Захватываем переменную для замыкания
            int id = consumerId;

            var consumerTask = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (queue.TryDequeue(out string? item))
                    {
                        Console.WriteLine($"[Consumer {id}] Processed: {item} [Thread: {Thread.CurrentThread.ManagedThreadId}]");
                        // Имитация обработки
                        Thread.Sleep(Random.Shared.Next(100, 300));
                    }
                    else
                    {
                        // Небольшая пауза если очередь пуста
                        Thread.Sleep(50);
                    }
                }
                Console.WriteLine($"[Consumer {id}] STOPPED");
            }, token);

            tasks.Add(consumerTask);
        }

        Console.WriteLine("Нажмите любую клавишу для остановки...");
        Console.ReadLine();

        // Останавливаем все потоки
        cancellationTokenSource.Cancel();

        // Ждем завершения всех задач
        try
        {
            Task.WaitAll(tasks.ToArray(), TimeSpan.FromSeconds(5));
        }
        catch (AggregateException)
        {
            Console.WriteLine("Некоторые задачи были отменены");
        }

        Console.WriteLine($"Осталось элементов в очереди: {queue.Count}");

        // Показываем оставшиеся элементы
        while (queue.TryDequeue(out string? remainingItem))
        {
            Console.WriteLine($"Необработанный элемент: {remainingItem}");
        }
    }
}
