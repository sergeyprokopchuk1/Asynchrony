using System;
using System.Threading.Tasks;

namespace _34.Task_Combinators
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += TaskException;

            //1. When Any
            //Task<int> win1 = await Task.WhenAny(Delay1(), Delay2(), Delay3());
            //Console.WriteLine(win1.Result);

            //int res1 = await await Task.WhenAny(Delay1(), Delay2(), Delay3());
            //Console.WriteLine(res1);

            //2. When Any with timeout

            //Task<int> t1 = Delay3();
            //Task winner = await Task.WhenAny(t1, Task.Delay(1000));
            //if(winner != t1)
            //{
            //    throw new TimeoutException();
            //}
            //int res2 = await t1;
            //Console.WriteLine(res2);

            //3. When All
            //await Task.WhenAll(Delay1(), Delay2(), Delay3());

            //4. All without When.All orginazing queue

            //Task t1 = Delay1();
            //Task t2 = Delay2();
            //Task t3 = Delay3();

            //await t1; await t2; await t3;

            //5. When.All catch exception

            Task t5 = Task.Run(() => throw null);
            Task t6 = Task.Run(() => throw null);

            Task all = Task.WhenAll(t5, t6);

            try
            {
                await all;
            }
            catch
            {
                Console.WriteLine("Count - " + all.Exception.InnerExceptions.Count);
            }

            Console.ReadKey();
        }

        private static async Task<int> Delay1()
        {
            throw null;

            await Task.Delay(1000);

            

            Console.WriteLine(1);

            return 1;
        }

        private static async Task<int> Delay2()
        {
            await Task.Delay(2000);

            Console.WriteLine(2);

            return 2;
        }

        private static async Task<int> Delay3()
        {
            await Task.Delay(3000);

            Console.WriteLine(3);

            return 3;
        }

        private static async Task<int> Delay4()
        {
            await Task.Delay(2000);

            throw null;

            Console.WriteLine(4);

            return 4;
        }

        static void TaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();

            ((AggregateException)e.Exception).Handle(ex =>
            {
                Console.WriteLine($"Message {ex.Message}");

                return true;
            });
        }
    }
}
