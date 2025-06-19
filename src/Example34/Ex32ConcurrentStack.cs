using System.Collections.Concurrent;

class Ex32ConcurrentStack
{
    public static void Run()
    {
        // Создание потокобезопасного стека
        var concurrentStack = new ConcurrentStack<int>();

        // Добавление - безопасно для многопоточности
        concurrentStack.Push(1);
        concurrentStack.Push(2);
        concurrentStack.Push(3);

        // Получение - только безопасные методы
        // Основной способ
        bool success = concurrentStack.TryPop(out int result);
        bool canPeek = concurrentStack.TryPeek(out int topValue);

        // ❌ НЕТ методов Pop() и Peek() - они небезопасны в многопоточности

        // Массовые операции
        var items = new[] { 4, 5, 6 };
        // Добавить массив
        concurrentStack.PushRange(items);

        var buffer = new int[3];
        // Извлечь до 3 элементов
        int poppedCount = concurrentStack.TryPopRange(buffer);

        // Проверки
        bool isEmpty = concurrentStack.IsEmpty;
        // Приблизительное значение!
        int count = concurrentStack.Count;

        // Перебор (снимок на момент создания перечислителя)
        foreach (int item in concurrentStack)
        {
            // Может не отражать текущее состояние
            Console.WriteLine(item);
        }
    }
}
