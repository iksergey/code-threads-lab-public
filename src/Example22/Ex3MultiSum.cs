class Ex3MultiSum
{
    public static void Run()
    {
        const int n = 1_000_000_000;
        int[] numbers = Enumerable.Range(1, n).ToArray();

        int partitionCount = 5;
        Task<decimal>[] workerTasks = new Task<decimal>[partitionCount];
        int chunkSize = n / partitionCount;

        for (int partitionIndex = 0; partitionIndex < partitionCount; partitionIndex++)
        {
            int chunkStart = partitionIndex * chunkSize;
            int chunkEnd = (partitionIndex == partitionCount - 1)
                ? n
                : chunkStart + chunkSize;

            workerTasks[partitionIndex] = Task.Run(() =>
            {
                decimal sum = 0;
                for (int i = chunkStart; i < chunkEnd; i++)
                {
                    sum += numbers[i];
                }
                return sum;
            });
        }
        Task.WaitAll(workerTasks);
        decimal totalSum = workerTasks.Sum(ch => ch.Result);
        Console.WriteLine($"totalSum: {totalSum.ToString("F0")}");
    }
}