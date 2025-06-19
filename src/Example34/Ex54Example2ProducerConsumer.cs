// Имитация блокирующей коллекции на основе обычной очереди
using System.Collections.Concurrent;

class Ex54Example2ProducerConsumer
{
    public static void Run()
    {
        var collection = new BlockingCollection<int>(10);

        // Несколько производителей
        for (int producerId = 0; producerId < 3; producerId++)
        {
            int id = producerId;
            Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    collection.Add(id * 100 + i);
                }
            });
        }

        // Несколько потребителей
        for (int consumerId = 0; consumerId < 2; consumerId++)
        {
            int id = consumerId;
            Task.Run(() =>
            {
                foreach (var item in collection.GetConsumingEnumerable())
                {
                    Console.WriteLine($"Consumer {id} processed: {item}");
                }
            });
        }

        // Завершение через некоторое время
        Task.Delay(1000).ContinueWith(_ => collection.CompleteAdding());
    }
}
