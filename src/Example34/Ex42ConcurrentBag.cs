using System.Collections.Concurrent;

class Ex42ConcurrentBag
{
    public static void Run()
    {
        // Создание неупорядоченной потокобезопасной коллекции
        var concurrentBag = new ConcurrentBag<int>();

        // Добавление элементов
        concurrentBag.Add(1);
        concurrentBag.Add(2);
        concurrentBag.Add(3);

        // ❌ НЕТ индексного доступа - bag[0] недоступно
        // ❌ НЕТ Remove(item) - только TryTake()

        // Извлечение элементов (порядок НЕ гарантируется)
        bool success = concurrentBag.TryTake(out int result);

        // Проверка без извлечения
        bool canPeek = concurrentBag.TryPeek(out int peekValue);

        // Проверки
        bool isEmpty = concurrentBag.IsEmpty;
        // Приблизительное значение!
        int count = concurrentBag.Count;

        // Массовые операции
        var items = new[] { 4, 5, 6 };
        foreach (var item in items)
        {
            concurrentBag.Add(item);
        }

        // Перебор (порядок НЕ гарантируется)
        foreach (int item in concurrentBag)
        {
            // Может быть любой порядок
            Console.WriteLine(item);
        }

        // Копирование в массив
        int[] array = concurrentBag.ToArray();
    }
}
