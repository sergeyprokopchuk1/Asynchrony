using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace _21.Task_Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Task<int> task = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n => 
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(a => n % a > 0)
                )
            );*/

            //Task<int> task = Task.Run(() =>{ throw null; return 1; });

            //TaskAwaiter<int> awaiter = task.GetAwaiter();
            //awaiter.OnCompleted(() => 
            //{
            //    try
            //    {
            //        int res = awaiter.GetResult();
            //        Console.WriteLine(res);
            //    }
            //    catch(NullReferenceException e)
            //    {
            //        var ex = e;
            //        Console.WriteLine("NullReferenceException");
            //    }
            //});

            //Console.ReadLine();

            //2

            try
            {
                var t1 = Task.Run(() => Task.Delay(2000).ContinueWith(c => throw null));
                var t2 = t1.ContinueWith(c =>
                    {
                        if(c.IsFaulted)
                        {
                            //throw c.Exception;
                        }
                    
                    })
                    ;

                if(t2.IsFaulted)
                {
                    throw t2.Exception;
                }
            }
            catch(Exception e)
            {
                var ex = e;
            }

            Console.ReadLine();
        }
    }
}
