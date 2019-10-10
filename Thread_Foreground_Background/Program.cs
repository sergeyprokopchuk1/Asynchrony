using System;
using System.Threading;

namespace Thread_Foreground_Background
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Foreground - waiting for pressing enter
            //background - thead is finishing
            //Thread t = new Thread(() => Console.ReadLine());
            //t.IsBackground = true;
            //t.Start();

            //2. finally block doesn't work in background thread
            Thread t2 = new Thread(Go2);
            t2.IsBackground = true;
            t2.Start();
            t2.Join(6000);
            Thread.Sleep(4000);
        }

        private static void Go2()
        {
            try
            {
                Console.WriteLine("Try works");
            }
            catch { }
            finally
            {
                Thread.Sleep(5000);
                Console.WriteLine("Finally works");
            }
        }
    }
}
