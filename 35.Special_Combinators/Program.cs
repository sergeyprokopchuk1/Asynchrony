using System;
using System.Threading;
using System.Threading.Tasks;

namespace _35.Special_Combinators
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //1. Combinator with timeout
            Task t = Task.Delay(4000);
            
            //await t.WithTimeout(TimeSpan.FromMilliseconds(3000));

            //2. Combinater with cancelation
            Task<int> t2 = Task.FromResult(1);
            //_ = t2.WithCancelation(CancellationToken.None);

            //3. Combinator as WnellAll but throw exception if at least one task failed.
            Task<int> t3 = Task.FromResult(1).ContinueWith(c => { throw null; return 0; });
            Task<int> t4 = Task.FromResult(1);

            var res = await Ext3.WhenAllOrError(t3, t4);

            Console.ReadKey();
        }
    }

    public static class Ext1
    {
        public static async Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            Task winner = await Task.WhenAny(task, Task.Delay(timeout));

            if (winner != task) throw new TimeoutException();

            return await task;
        }

        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            Task winner = await Task.WhenAny(task, Task.Delay(timeout));

            if (winner != task)
            {
                Console.WriteLine("Timeout faster");
                throw new TimeoutException(); 
            }

            await task;

            Console.WriteLine("Task faster");
        }
    }

    public static class Ext2
    {
        public static Task<TResult> WithCancelation<TResult>(this Task<TResult> task, CancellationToken cancelationToken)
        {
            var tcs = new TaskCompletionSource<TResult>();
            var reg = cancelationToken.Register(() => tcs.TrySetCanceled());

            task.ContinueWith(ant => 
            {
                reg.Dispose();

                if(ant.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else if(ant.IsFaulted)
                {
                    tcs.TrySetException(ant.Exception.InnerException);
                }
                else
                {
                    tcs.TrySetResult(ant.Result);
                }
            });

            return tcs.Task;
        }
    }

    public static class Ext3
    {
        public static async Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
        {
            var killJoy = new TaskCompletionSource<TResult[]>();

            foreach(var task in tasks)
            {
                _ = task.ContinueWith(ant =>
                {
                    if (ant.IsCanceled)
                    {
                        killJoy.TrySetCanceled();
                    }
                    else if (ant.IsFaulted)
                    {
                        killJoy.TrySetException(ant.Exception.InnerException);
                    }
                });
            }

            return await await Task.WhenAny(killJoy.Task, Task.WhenAll(tasks));
        }
    }
}
