using System.Collections.Concurrent;

class Ex22ConcurrentQueue
{
    public static void Run()
    {
        // Создание потокобезопасной очереди
        var concurrentQueue = new ConcurrentQueue<int>();

        // Добавление - всегда безопасно
        concurrentQueue.Enqueue(1);
        concurrentQueue.Enqueue(2);
        concurrentQueue.Enqueue(3);

        // Получение - только безопасные методы
        // Основной способ
        bool success = concurrentQueue.TryDequeue(out int result);
        bool canPeek = concurrentQueue.TryPeek(out int peekValue);

        // ❌ НЕТ методов Dequeue() и Peek() - они небезопасны в многопоточности

        // Проверки
        bool isEmpty = concurrentQueue.IsEmpty;
        // Приблизительное значение!
        int count = concurrentQueue.Count;

        // Массовые операции
        var items = new[] { 4, 5, 6 };
        foreach (var item in items)
        {
            concurrentQueue.Enqueue(item);
        }

        // Перебор (снимок на момент создания перечислителя)
        foreach (int item in concurrentQueue)
        {
            // Может не отражать текущее состояние
            Console.WriteLine(item);
        }
    }
}
