using System;
using System.Threading;

namespace _6.Thread_Signal
{
    class Program
    {
        //public delegate Action D();
        //private delegate Action A();
        public static ManualResetEvent signal = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Action d1 = S1;
            Action d2 = S2;

            new Thread(new ThreadStart(d1)).Start();

            new Thread(new ThreadStart(d2)).Start();

            //Thread.Sleep(5000);
            signal.Set();

            signal.Reset();

            Console.ReadLine();
        }

        public static void S1()
        {
            Console.WriteLine("Waiting for signal 1...");

            signal.WaitOne(5000);
            signal.Dispose();

            Console.WriteLine("Got signal 1!");
        }
        public static void S2()
        {
            Console.WriteLine("Waiting for signal 2...");

            signal.WaitOne(5000);
            signal.Dispose();

            Console.WriteLine("Got signal 2!");
        }
    }
}
