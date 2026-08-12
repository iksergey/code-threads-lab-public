namespace LinkedCancellationTokensExample;

public class Example
{
    private static CancellationTokenSource _commonCts = new();
    private static Dictionary<string, CancellationTokenSource> _commands = new();
    private static List<Task> _activeTasks = new();
    private static readonly object _lockObj = new();

    public static void Run()
    {
        Console.WriteLine("Сервер запущен...");
        Console.WriteLine("  <command>           - выполнить команду (задержка 10 секунд)");
        Console.WriteLine("  cancel <command>    - отменить выполнение <command>");
        Console.WriteLine("  q                   - выход (выполнение всех команд будет отменено)\n");

        while (true)
        {
            string? request = Console.ReadLine();

            if (request == null)
            {
                continue;
            }

            if (request.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                _commonCts.Cancel();
                break;
            }

            if (request.StartsWith("cancel ", StringComparison.OrdinalIgnoreCase))
            {
                string command = request.Substring(7).Trim();
                CancelCommand(command);
                continue;
            }

            StartCommand(request);
        }

        Task.WaitAll([.. _activeTasks]);

        lock (_lockObj)
        {
            foreach (var cts in _commands.Values)
            {
                cts.Dispose();
            }

            _commands.Clear();
        }
        _commonCts.Dispose();

        Console.WriteLine("Приложение завершено.");
    }

    private static void StartCommand(string command)
    {
        lock (_lockObj)
        {
            if (_commands.ContainsKey(command))
            {
                Console.WriteLine($"Команда '{command}' уже выполняется.");
                return;
            }

            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_commonCts.Token);
            _commands[command] = linkedCts;

            Task task = Task.Run(() => HandleRequest(command, linkedCts.Token), linkedCts.Token);

            _activeTasks.Add(task);
        }
    }

    private static void CancelCommand(string command)
    {
        CancellationTokenSource ctsToCancel = null;

        lock (_lockObj)
        {
            if (_commands.TryGetValue(command, out var cts))
            {
                ctsToCancel = cts;
            }
        }

        if (ctsToCancel != null)
        {
            ctsToCancel.Cancel();
            Console.WriteLine($"Запрошена отмена команды: {command}");
        }
        else
        {
            Console.WriteLine($"Команда '{command}' не найдена.");
        }
    }

    private static void HandleRequest(string command, CancellationToken token)
    {
        try
        {
            Console.WriteLine($"Начато выполнение задачи: {command}");

            if (token.WaitHandle.WaitOne(TimeSpan.FromSeconds(10)))
            {
                Console.WriteLine($"Задача {command} отменена.");
                return;
            }

            Console.WriteLine($"Обработан запрос: {command}");
        }
        finally
        {
            lock (_lockObj)
            {
                if (_commands.TryGetValue(command, out var existingCts))
                {
                    _commands.Remove(command);
                    existingCts.Dispose();
                }
            }
            Console.WriteLine($"Task {Task.CurrentId} для команды: {command} завершена");
        }
    }
}
