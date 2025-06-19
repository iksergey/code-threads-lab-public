using System.Collections.Concurrent;

class TrainTicketBooking
{
    private BlockingCollection<string> requestQueue = new BlockingCollection<string>();
    private int seatsAvailable = 5;
    private readonly int seats = 5;
    private readonly object seatsLock = new();

    private void HandleRequests()
    {
        foreach (var command in requestQueue.GetConsumingEnumerable())
        {
            ThreadPool.QueueUserWorkItem(cmd =>
                ProcessCommand((string)cmd!), command);
        }
    }

    private void ProcessCommand(string command)
    {
        Thread.Sleep(2000);

        lock (seatsLock)
        {
            if (command == "1")
            {
                if (seatsAvailable > 0)
                {
                    seatsAvailable--;
                    Console.WriteLine($"\nЗабронировано. Осталось  {seatsAvailable}");
                }
                else
                {
                    Console.WriteLine("\nВагон заполнен");
                }
            }
            else if (command == "2")
            {
                if (seatsAvailable < seats)
                {
                    seatsAvailable++;
                    Console.WriteLine($"\nБронь отменена. Свободно: {seatsAvailable}");
                }
                else
                {
                    Console.WriteLine("\nНечего отменять");
                }
            }
            else
            {
                Console.WriteLine("\nОшибка.");
            }
        }
    }

    public void Run()
    {
        Thread handlerThread = new Thread(HandleRequests);
        handlerThread.Start();

        Console.WriteLine("Бронирования запущено");
        Console.WriteLine("Нажмите 1 чтобы забронировать");
        Console.WriteLine("Нажмите 2 чтобы отменить бронь");

        while (true)
        {
            string command = Console.ReadLine()!;
            if (command?.ToLower() == "q")
            {
                requestQueue.CompleteAdding();
                break;
            }

            requestQueue.Add(command!);
        }

        handlerThread.Join();
    }
}
