using MatrixAddition;
// Main.Run(args); // Запуск dotnet run -- 50000 50000 5 false
//Main.Run("10 10 5 false".Split(" "));

int count = 5;

int size = 15_000;

while (count-- > 0)
{
    Console.Write($"Попытка {5 - count,-2}: ");
    Main.Run($"{size} {size} 5 false".Split(" "));
    Console.WriteLine();
}
