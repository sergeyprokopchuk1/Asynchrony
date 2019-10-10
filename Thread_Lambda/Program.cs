using System;
using System.Threading;

namespace Thread_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. non-deterministic output
            for (int i = 0; i < 10; i++)
            {
                new Thread(() => Console.Write(i)).Start();
            }

            Console.WriteLine();

            //2. deterministic output
            for (int i = 0; i < 10; i++)
            {
                int t = i;
                new Thread(() => Console.Write(t)).Start();
            }

            Console.WriteLine();

            //3. s2 is shown twice
            string s = "s1";
            Thread t1 = new Thread(() => Console.WriteLine(s));

            s = "s2";

            Thread t2 = new Thread(() => Console.WriteLine(s));

            t1.Start();
            t2.Start();
        }
    }
}
