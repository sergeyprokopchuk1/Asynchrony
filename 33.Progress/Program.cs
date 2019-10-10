using System;
using System.Threading.Tasks;

namespace _33.Progress
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //1. Progress using delegates. Bad solution for clients apps
            //Action<int> progress = i => Console.WriteLine(i + " %");
            //await Progress1(progress);

            //2. For clients apps through synchronization context
            IProgress<int> p2 = new Progress<int>(i => { Console.WriteLine(i + " %"); });
            await Progress2(p2);
        }

        private static Task Progress1(Action<int> onProgress)
        {
            return Task.Run(async () => 
            {
                for (int i = 0; i < 1000; i++)
                {
                    if(i % 10 == 0)
                    {
                        onProgress(i/10);
                    }

                    await Task.Delay(100);
                }
            });
        }

        private static Task Progress2(IProgress<int> onProgress)
        {
            return Task.Run(async () => 
            {
                for (int i = 0; i < 1000; i++)
                {
                    if(i % 10 == 0)
                    {
                        onProgress.Report(i/10);
                    }

                    await Task.Delay(100);
                }
            });
        }
    }
}
