// Имитация блокирующей коллекции на основе обычной очереди
using System.Collections.Concurrent;

class Ex53Example1ProducerConsumer
{
    public static void Run()
    {
        var collection = new BlockingCollection<string>(boundedCapacity: 5);

        // Producer (производитель)
        Task.Run(() =>
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    string item = $"Item {i}";
                    collection.Add(item); // Блокируется если коллекция переполнена
                    Console.WriteLine($"Produced: {item}");
                    Thread.Sleep(100);
                }
            }
            finally
            {
                collection.CompleteAdding(); // Важно! Сигнализируем о завершении
            }
        });

        // Consumer (потребитель)
        Task.Run(() =>
        {
            try
            {
                while (!collection.IsCompleted)
                {
                    if (collection.TryTake(out string? item, TimeSpan.FromMilliseconds(500)))
                    {
                        Console.WriteLine($"Consumed: {item}");
                        Thread.Sleep(200); // Имитация медленной обработки
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // Коллекция была закрыта
                Console.WriteLine("Collection was completed");
            }
        });

    }
}
