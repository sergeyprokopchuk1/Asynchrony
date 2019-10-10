using System;
using System.Threading.Tasks;

namespace _27.Asynchronous_Graph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var t = Go();
            await t;
        }

        static async Task Go()
        {
            var t = Print();
            await t;
            Console.WriteLine("Done");
        }

        static async Task Print()
        {
            var t = Get();
            int res = await t;

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
