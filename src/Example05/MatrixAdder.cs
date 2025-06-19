using System.Diagnostics;

namespace MatrixAddition
{
    public class MatrixAdder
    {
        private readonly Matrix a, b, c;
        private readonly int threadCount;

        public MatrixAdder(Matrix a, Matrix b, int threadCount)
        {
            this.a = a;
            this.b = b;
            this.threadCount = threadCount;
            c = new Matrix(this.a.Rows, this.a.Cols);
        }

        public (Matrix result, long sequentialMs) AddSequential()
        {
            int m = a.Rows, n = a.Cols;
            var sw = Stopwatch.StartNew();

            var result = new Matrix(m, n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }

            sw.Stop();
            return (result, sw.ElapsedMilliseconds);
        }

        public (Matrix result, long totalMs, long parallelMs) Add()
        {
            int m = a.Rows, n = a.Cols;
            int chunkSize = m / threadCount;
            int remainder = m % threadCount;

            var threads = new Thread[threadCount];
            var swTotal = Stopwatch.StartNew();
            var swParallel = new Stopwatch();

            int startRow = 0;
            swParallel.Start();

            for (int t = 0; t < threadCount; t++)
            {
                int currentSize = chunkSize + (t < remainder ? 1 : 0);
                int rowBegin = startRow;
                int rowEnd = startRow + currentSize;
                startRow += currentSize;

                threads[t] = new Thread(() =>
                {
                    for (int i = rowBegin; i < rowEnd; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            c[i, j] = a[i, j] + b[i, j];
                        }
                    }
                });
                threads[t].Start();
            }

            foreach (var thr in threads)
            {
                thr.Join();
            }

            swParallel.Stop();
            swTotal.Stop();

            return (c, swTotal.ElapsedMilliseconds, swParallel.ElapsedMilliseconds);
        }
    }
}
