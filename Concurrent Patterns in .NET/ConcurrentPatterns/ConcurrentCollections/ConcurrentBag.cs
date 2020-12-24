using System;
using System.Threading;
using System.Threading.Tasks;


namespace ConcurrentCollections
{
    public class ConcurrentBag
    {

        public void RunCancellationToken()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                if (Console.ReadKey().KeyChar == 'c' || Console.ReadKey().KeyChar == 'C')
                {

                }
            });
        }

        
    }
}
