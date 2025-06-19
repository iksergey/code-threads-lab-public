using System.Collections.Concurrent;

class Ex43Example
{
    public static void Run()
    {
        var results = new ConcurrentBag<string>();

        // Параллельная обработка данных
        Parallel.For(0, 1000, i =>
        {
            // Какая-то обработка
            string result = ProcessItem(i);
            results.Add(result); // Thread-safe добавление
        });

        // Извлечение всех результатов
        var allResults = new List<string>();
        while (results.TryTake(out string? item))
        {
            allResults.Add(item);
        }

        Console.WriteLine($"Обработано {allResults.Count} элементов");
    }

    public static string ProcessItem(int item)
    {
        double result = Math.Sin(item) * Math.Cos(item) + Math.Sqrt(item);
        return $"{result}";
    }
}
