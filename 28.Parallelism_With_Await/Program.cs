using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _28.Parallelism_With_Await
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //1. Asynchronous work of asynchronous methods
            Stopwatch w1 = new Stopwatch();
            w1.Start();

            await Print();
            Console.WriteLine("Between 1");
            await Print();
            
            w1.Stop();
            Console.WriteLine("Async Milliseconds - " + w1.ElapsedMilliseconds);

            //2. Parallel work of asynchronous methods
            Stopwatch w2 = new Stopwatch();
            w2.Start();

            var t1 = Print();
            var t2 = Print();

            await t1;
            Console.WriteLine("Between 2");
            await t2;

            w2.Stop();
            Console.WriteLine("Parallel Milliseconds - " + w2.ElapsedMilliseconds);
        }

        static async Task Print()
        {
            var t = Get();
            var res = await t;

            Console.WriteLine(res);
        }

        static async Task<int> Get()
        {
            var t = Task.Delay(5000);
            await t;

            return 21;
        }
    }
}
