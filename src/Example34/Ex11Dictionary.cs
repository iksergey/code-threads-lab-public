class Ex11Dictionary
{
    public static void Run()
    {
        // Создание и основные операции
        var dictionary = new Dictionary<string, int>();

        // Добавление элементов
        dictionary.Add("apple", 5);
        dictionary["banana"] = 3;
        dictionary["orange"] = 8;

        // Получение значений
        // Бросит KeyNotFoundException если ключа нет
        int appleCount = dictionary["apple"];

        // Безопасный способ
        bool hasValue = dictionary.TryGetValue("grape", out int grapeCount);

        // Проверка существования
        bool hasApple = dictionary.ContainsKey("apple");
        bool hasValue5 = dictionary.ContainsValue(5);

        // Обновление
        // Перезаписывает существующее значение
        dictionary["apple"] = 10;

        // Удаление
        bool removed = dictionary.Remove("banana");
        // Очищает весь словарь
        dictionary.Clear();

        // Перебор
        foreach (var kvp in dictionary)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}
