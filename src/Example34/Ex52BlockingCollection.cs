// Имитация блокирующей коллекции на основе обычной очереди
using System.Collections.Concurrent;

class Ex52BlockingCollection
{
    public static void Run()
    {
        // Создание блокирующей коллекции
        var blockingCollection = new BlockingCollection<int>();

        // С ограничением размера (bounded capacity)
        var boundedCollection = new BlockingCollection<int>(boundedCapacity: 10);

        // С кастомной внутренней коллекцией
        var stackBasedCollection = new BlockingCollection<int>(new ConcurrentStack<int>());

        // Добавление элементов
        // Блокируется если коллекция заполнена
        blockingCollection.Add(1);
        blockingCollection.Add(2);
        // С таймаутом
        bool added = blockingCollection.TryAdd(3, TimeSpan.FromSeconds(1));

        // Извлечение элементов
        // Блокируется если коллекция пуста
        int item = blockingCollection.Take();
        // С таймаутом
        bool taken = blockingCollection.TryTake(out int result, TimeSpan.FromSeconds(1));

        // Завершение добавления
        // Сигнализирует что больше элементов не будет
        blockingCollection.CompleteAdding();

        blockingCollection.GetConsumingEnumerable();

        // Проверки
        bool isCompleted = blockingCollection.IsCompleted;
        bool isAddingCompleted = blockingCollection.IsAddingCompleted;
        int count = blockingCollection.Count;

    }
}
