namespace MatrixAddition
{
    // 3. Класс Program: парсинг аргументов, инициализация, запуск и вывод
    public class Main
    {
        public static void Run(string[] args)
        {
            int m, n, T;
            bool view;

            if (args.Length >= 3
                && int.TryParse(args[0], out m)
                && int.TryParse(args[1], out n)
                && int.TryParse(args[2], out T)
                && bool.TryParse(args[3], out view))
            {
                // Параметры заданы через командную строку
            }
            else
            {
                Console.Write("Введите число строк m (1-50000): ");
                m = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Введите число столбцов n (1-50000): ");
                n = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Введите число потоков T (1 ≤ T ≤ m): ");
                T = int.Parse(Console.ReadLine() ?? "0");
                view = true;
            }

            if (m < 1 || m > 50000 || n < 1 || n > 50000 || T < 1 || T > m)
            {
                Console.Error.WriteLine("Ошибка: неверные параметры.");
                return;
            }

            var A = new Matrix(m, n);
            var B = new Matrix(m, n);
            A.FillRandom();
            B.FillRandom();

            var adder = new MatrixAdder(A, B, T);
            var (C, totalTime, parallelTime) = adder.Add();

            Console.WriteLine($"Общее время работы: {totalTime} мс");
            Console.WriteLine($"Время многопоточной части: {parallelTime} мс");

            var (result, time) = adder.AddSequential();
            Console.WriteLine($"Общее время работы: {time} мс");


            // Вывод матрицы C (можно отключить для больших размеров)
            Console.WriteLine("Результат C = A + B:");

            if (view)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                        Console.Write($"{C[i, j],5}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Посчитан");
            }
        }
    }
}
