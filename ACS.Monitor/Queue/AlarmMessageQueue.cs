using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Monitor
{
   public class AlarmMessageQueue <T>
    {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        public int Count => _queue.Count;

        public void Enqueue(T item) => _queue.Enqueue(item);

        public bool TryDequeue(out T item) => _queue.TryDequeue(out item);

        public bool TryPeek(out T item) => _queue.TryPeek(out item);
    }
}
