using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    public class Queues
    {
        public int GetSumOfIntegersInQueue(int totalValue)
        {
            ConcurrentQueue<int> cq = new ConcurrentQueue<int>();

            //Populate the queue
            Parallel.For(0, totalValue, (index) =>
            {
                cq.Enqueue(index);
            });

            int outerSum = 0;

            //An action to consume the ConcurrentQueue
            Action action = () =>
            {
                int localSum = 0;
                int localValue;
                while (cq.TryDequeue(out localValue))
                {
                    localSum += localValue;
                }
                Interlocked.Add(ref outerSum, localSum);
            };
            //Start 4 concurrent consuming actions.
           
            Parallel.Invoke(action, action, action, action);

            return outerSum;
        }
    }
}
