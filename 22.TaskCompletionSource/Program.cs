using System;
using System.Threading;
using System.Threading.Tasks;

namespace _22.TaskCompletionSource
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //1. Creating
            /*TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

            new Thread(() =>
            {
                Thread.Sleep(5000); tcs.SetResult(42);
            })
            { IsBackground = true }
            .Start();

            //подчиненная задача
            Task<int> task = tcs.Task;

            Console.WriteLine(task.Result);*/

            //2. Creating own Run mwthod using TaskCompletionSource
            //Func<int> func1 = () => { Thread.Sleep(5000); return 43; };
            //Func<int> func2 = () => { Thread.Sleep(5000); return 44; };

            //Task<int> task = Run(func1);
            //int res = await Run(func2);
            //Console.WriteLine(task.Result);
            //Console.WriteLine(res);

            //3. without blocking threads
            //Func<int> func1 = () => { return 46; };

            //var awaiter = GetAnswerToLife().GetAwaiter();
            //awaiter.OnCompleted(() => { Console.WriteLine(awaiter.GetResult()); });

            //var awaiter2 = GetAnswerToLife(func1).GetAwaiter();
            //awaiter2.OnCompleted(() => { Console.WriteLine(awaiter2.GetResult()); });

            //Console.ReadLine();

            //4. Task.Delay is asynchronious Thread.Sleep
            for (int i = 0; i < 5000; i++)
            {
                Task.Delay(5000).GetAwaiter().OnCompleted(() => { Console.WriteLine(47); });
                _ = Task.Delay(5000).ContinueWith(c => { Console.WriteLine(48); });
            }

            Console.ReadLine();
        }

        private static Task<TResult> Run<TResult> (Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();

            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(function());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }).Start();

            return tcs.Task;
        }

        private static Task<int> GetAnswerToLife()
        {
            var tcs = new TaskCompletionSource<int>();

            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed +=  delegate 
            {
                timer.Dispose();
                tcs.SetResult(45);
            };
            timer.Start();

            return tcs.Task;
        }

        private static Task<TResult> GetAnswerToLife<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();

            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed += delegate
            {
                timer.Dispose();
                tcs.SetResult(function());
            };
            timer.Start();

            return tcs.Task;
        }
    }
}
