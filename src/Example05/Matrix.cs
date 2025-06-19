namespace MatrixAddition
{
    public class Matrix
    {
        private readonly int[,] data;
        public int Rows { get; }
        public int Cols { get; }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            data = new int[rows, cols];
        }

        public int this[int i, int j]
        {
            get => data[i, j];
            set => data[i, j] = value;
        }

        public void FillRandom(int minValue = -100, int maxValue = 100)
        {
            var random = Random.Shared;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    data[i, j] = random.Next(minValue, maxValue + 1);
                }
            }
        }
    }
}
