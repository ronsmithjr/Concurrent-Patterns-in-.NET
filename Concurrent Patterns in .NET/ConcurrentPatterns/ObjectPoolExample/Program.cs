using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ConcurrentModels.Models;

namespace ObjectPoolExample
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                if(Console.ReadKey().KeyChar == 'c' || Console.ReadKey().KeyChar == 'C')
                {
                    cts.Cancel();
                }
            });

            ObjectPool<MyClass> pool = new ObjectPool<MyClass>(() => new MyClass());

            Parallel.For(0, 1000000, (i, loopState) =>
            {
                MyClass mc = pool.GetObject();
                Console.CursorLeft = 0;

                Console.WriteLine("{0:####.####}", mc.GetValue(i));

                pool.PutObject(mc);
                if(cts.Token.IsCancellationRequested)
                {
                    loopState.Stop();
                }


            });
            Console.WriteLine("Press the enter key to exit");
            Console.ReadLine();
            cts.Dispose();
        }
    }

    class MyClass
    {
        public int[] Nums { get; set; }
        public double GetValue(long i)
        {
            return Math.Sqrt(Nums[i]);
        }
        public MyClass()
        {
            Nums = new int[1000000];
            Random rand = new Random();
            for (int i = 0; i < Nums.Length; i++)
            {
                Nums[i] = rand.Next();
            }
        }
    }
}
