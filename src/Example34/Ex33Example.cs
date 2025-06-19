using System.Collections.Concurrent;

class Ex33Example
{
    public static void Run()
    {
        var stack = new ConcurrentStack<string>();

        // Добавление задач
        Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                stack.Push($"Task {i}");
                Console.WriteLine($"Pushed: Task {i}");
                Thread.Sleep(100);
            }
        });

        // Обработка задач (LIFO - последние добавленные обрабатываются первыми)
        Task.Run(() =>
        {
            while (true)
            {
                if (stack.TryPop(out string? task))
                {
                    Console.WriteLine($"Processing: {task}");
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        });
    }
}
