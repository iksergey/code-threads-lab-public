class Ex31Stack
{
    public static void Run()
    {
        // Создание стека LIFO (Last In, First Out)
        var stack = new Stack<int>();

        // Добавление элементов (на вершину)
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        // Получение элементов (с вершины)
        // Возвращает 3, удаляет из стека
        int last = stack.Pop();
        // Возвращает 2, НЕ удаляет
        int top = stack.Peek();

        // Проверки
        bool hasElements = stack.Count > 0;
        int count = stack.Count;

        // Очистка
        stack.Clear();

        // Перебор (от вершины к основанию)
        foreach (int item in stack)
        {
            // 2, 1 (если Pop() был вызван)
            Console.WriteLine(item);
        }

        // Преобразование в массив
        // Порядок от вершины к основанию
        int[] array = stack.ToArray();

    }
}
