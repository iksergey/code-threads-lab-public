class Ex21Queue
{
    public static void Run()
    {
        // Создание очереди FIFO (First In, First Out)
        var queue = new Queue<int>();

        // Добавление элементов (в конец)
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        // Получение элементов (из начала)
        // Возвращает 1, удаляет из очереди
        int first = queue.Dequeue();
        // Возвращает 2, НЕ удаляет
        int next = queue.Peek();

        // Проверки
        bool hasElements = queue.Count > 0;
        bool hasValue = queue.TryDequeue(out int value);
        bool canPeek = queue.TryPeek(out int peekValue);

        // Очистка
        queue.Clear();

        // Перебор (не изменяет очередь)
        foreach (int item in queue)
        {
            Console.WriteLine(item);
        }
    }
}
