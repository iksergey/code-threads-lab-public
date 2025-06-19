using System.Collections.Concurrent;

class Ex12ConcurrentDictionary
{
    public static void Run()
    {
        // Создание
        var concurrentDict = new ConcurrentDictionary<string, int>();

        // Добавление - только безопасные методы
        bool added = concurrentDict.TryAdd("apple", 5);
        // Добавляет или обновляет
        concurrentDict["banana"] = 3;

        // Получение - те же методы
        int appleCount = concurrentDict["apple"];
        bool hasValue = concurrentDict.TryGetValue("grape", out int grapeCount);

        // Атомарные операции
        int newValue = concurrentDict.AddOrUpdate("apple",
            1,                    // Значение для добавления если ключа нет
            (key, oldValue) => oldValue + 1); // Функция обновления

        // Получить или добавить
        int result = concurrentDict.GetOrAdd("orange", 8);

        // Удаление
        bool removed = concurrentDict.TryRemove("banana", out int removedValue);

        // Условное обновление
        // Обновить только если текущее значение = 10
        bool updated = concurrentDict.TryUpdate("apple", 15, 10);
    }
}
