class TrainTicketBooking
{
    private Queue<string> requestQueue = new Queue<string>();
    private int seatsAvailable = 5;
    private readonly int seats = 5;
    private bool isRunning = true;
    private readonly Lock seatsLock = new();

    private void HandleRequests()
    {
        while (isRunning || requestQueue.Count > 0)
        {
            string command = default!;
            lock (requestQueue)
            {
                if (requestQueue.Count > 0)
                {
                    command = requestQueue.Dequeue();
                }
            }

            if (command is not null)
            {
                new Thread(() => ProcessCommand(command)).Start();
            }
            else
            {
                Thread.Sleep(10);
            }
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
                isRunning = false;
                break;
            }

            lock (requestQueue)
            {
                requestQueue.Enqueue(command!);
            }
        }

        handlerThread.Join();
    }
}
