using System;
using System.Threading;
using System.Threading.Tasks;

namespace _32.Cancel
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //1. cancel
            var cancelSource = new CancellationTokenSource(5000);
            var t = Method(cancelSource.Token);

            //await Task.Delay(2000);

            cancelSource.Cancel();

            Console.ReadKey();
        }

        static async Task Method(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
