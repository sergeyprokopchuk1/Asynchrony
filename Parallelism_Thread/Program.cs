using System;
using System.Threading;

namespace Parallelism_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadStart s = new ThreadStart((WriteY);
            ParameterizedThreadStart ps = new ParameterizedThreadStart(WriteY);
            Thread t = new Thread(ps);
            t.Start(1000);
            //t.Join();

            for (int i = 0; i < 1000; i++)
            {
                Console.Write("x");
            }
        }

        static void WriteY(object len)
        {
            for (int i = 0; i < (int)len; i++)
            {
                Console.Write("y");
            }
        }
    }
}