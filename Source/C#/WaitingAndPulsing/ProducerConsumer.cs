using System.Collections.Generic;
using System.Threading;

namespace WaitingAndPulsing
{
    public class ProducerConsumer<T>
    {
        public void Produce(T t)
        {
            lock (_queueLock)
            {
                _queue.Enqueue(t);
                Monitor.Pulse(_queueLock);
            }
        }

        public object Consume()
        {
            lock (_queueLock)
            {
                while (_queue.Count == 0)
                {
                    // This releases _listLockand reacquires it
                    // after being woken up by a call to Pulse
                    Monitor.Wait(_queueLock);
                }
                return _queue.Dequeue();
            }
        }

        private readonly object _queueLock = new object();

        private readonly Queue<T> _queue = new Queue<T>();
    }
}