using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Thread_Exceptions
{
    class Program
    {
        private static ConcurrentQueue<Exception> _e = new ConcurrentQueue<Exception>();
        static void Main(string[] args)
        {
            //1. 
            //try
            //{
            //    new Thread(() => throw null).Start();
            //}
            //catch(Exception e)
            //{
            //    //never come here
            //    Console.WriteLine(e.Message);
            //}

            //2.
            //try
            //{
            //    new Thread(Go).Start();
            //}
            //catch (Exception e)
            //{
            //    //never come here
            //    Console.WriteLine(e.Message);
            //}

            //3.
            //new Thread(Go3).Start();

            //4.
            new Thread(Go4).Start();

            Thread.Sleep(5000);

            if(_e.Count > 0)
            {
                foreach(var ex in _e)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void Go()
        {
            throw null;
        }

        static void Go3()
        {
            try
            {
                throw null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Go4()
        {
            try
            {
                throw null;
            }
            catch(Exception e)
            {
                _e.Enqueue(e);
            }
        }
    }
}
