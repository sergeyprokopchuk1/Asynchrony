using System;
using System.Threading;
using System.Threading.Tasks;

namespace _7.Thread_Pool
{
    class Program
    {
        static void Main(string[] args)
        {
            Action a1 = () => Console.WriteLine("Hello from the thead pool, from run!");
            Action a2 = Cons;

            Task.Run(a1);
            Task.Run(a2);

            var i = ThreadPool.ThreadCount;

            WaitCallback wait = new WaitCallback(p => Console.WriteLine($"Hello Thread Pool! Count{i}"));

            ThreadPool.QueueUserWorkItem(wait /*p => Console.WriteLine($"Hello Thread Pool! Count{i}")*/);

            Console.ReadLine();
        }

        public static void Cons()
        {
            Console.WriteLine("Hello from the thead pool, from run!");
        }
    }
}
