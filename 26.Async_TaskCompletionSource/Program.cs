using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace _26.Async_TaskCompletionSource
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            await PrintAnswerToLife();

            Console.ReadKey();
        }

        private static Task PrintAnswerToLife()
        {
            var tcs = new TaskCompletionSource<object>();

            TaskAwaiter awaiter = Task.Delay(5000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                try
                {
                    //Generate an exception again
                    awaiter.GetResult();

                    int answer = 21 * 2;
                    Console.WriteLine(answer);

                    tcs.SetResult(null);
                }
                catch(Exception e)
                {
                    tcs.SetException(e);
                }
            });

            return tcs.Task;
        }
    }
}
