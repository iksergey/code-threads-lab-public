class Ex41List
{
    public static void Run()
    {
        // Создание списка
        var list = new List<int>();

        // Добавление элементов
        list.Add(1);
        list.Add(2);
        list.Add(3);

        // Доступ по индексу
        int first = list[0];
        int last = list[list.Count - 1];

        // Удаление элементов
        // Удаляет первое вхождение
        bool removed = list.Remove(2);
        // Удаляет по индексу
        list.RemoveAt(0);

        // Проверки
        bool contains = list.Contains(3);
        int count = list.Count;
        int index = list.IndexOf(2);

        // Перебор в определенном порядке
        foreach (int item in list)
        {
            // Порядок добавления сохраняется
            Console.WriteLine(item);
        }
    }
}
