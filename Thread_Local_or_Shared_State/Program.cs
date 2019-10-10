using System;
using System.Threading;

namespace Thread_Local_or_Shared_State
{
    class Program
    {
        bool _done;
        static bool _done4;
        static bool _done5;

        static readonly object _locker = new object();
        static void Main(string[] args)
        {
            //1. local state - with local variable
            //new Thread(Go1).Start();
            //Go1();

            //2. shared state 
            //Program pp = new Program();
            //new Thread(pp.Go2).Start();
            //pp.Go2();

            //3. shared state - anonymous delegates and lambda-expressions are converted to fields
            //bool done = false;
            //ThreadStart action = () =>
            //{
            //    if(!done)
            //    {
            //        done = true;
            //        Console.WriteLine("Done");
            //    }
            //};
            //new Thread(action).Start();
            //action();

            //4. static fields are shared between all threads in the same app domain
            new Thread(Go4).Start();
            Go4();

            //5. thread blocking and security
            new Thread(Go5).Start();
            Go5();
        }

        static void Go1()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write('?');
            }
        }

        void Go2()
        {
            if(!_done)
            {
                _done = true;
                Console.WriteLine("Done");
            }
        }

        static void Go4()
        {
            if(!_done4)
            {
                Console.WriteLine("Done4");
                _done4 = true;
            }
        }

        static void Go5()
        {
            lock (_locker)
            {
                if (!_done5)
                {
                    Console.WriteLine("Done5");
                    _done5 = true;
                }
            }
        }
    }
}
