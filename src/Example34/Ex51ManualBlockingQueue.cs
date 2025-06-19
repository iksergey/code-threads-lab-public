// Имитация блокирующей коллекции на основе обычной очереди
public class Ex51ManualBlockingQueue<T>
{
    private readonly Queue<T> queue = new();
    private readonly object lockObj = new();
    private readonly ManualResetEventSlim signal = new(false);

    public void Add(T item)
    {
        lock (lockObj)
        {
            queue.Enqueue(item);
            // Сигнализируем о появлении элемента
            signal.Set();
        }
    }

    public T Take()
    {
        while (true)
        {
            lock (lockObj)
            {
                if (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    if (queue.Count == 0)
                        // Сбрасываем сигнал если очередь пуста
                        signal.Reset();
                    return item;
                }
            }
            // Блокируемся до появления элементов
            signal.Wait();
        }
    }
}
