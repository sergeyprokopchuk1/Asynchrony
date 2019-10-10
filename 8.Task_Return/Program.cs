using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _8.Task_Return
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Task return
            Func<int> func = () => { Console.WriteLine("Hello"); Thread.Sleep(5000); return 3; };

            Task<int> task = Task.Run(func);

            int res = task.Result;

            Console.WriteLine(res);

            Console.ReadLine();

        }
    }
}
