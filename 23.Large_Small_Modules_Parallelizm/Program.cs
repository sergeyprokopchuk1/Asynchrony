using System;
using System.Linq;
using System.Threading.Tasks;

namespace _23.Large_Small_Modules_Parallelizm
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //1. Simple run
            //DisplayPrimeCounts();

            //2. Large module parallelizm
            //Task.Run(() => DisplayPrimeCounts());

            //3. Small module parallelizm
            await DisplayPrimeCountsAsync();
        }

        private static int GetPrimesCount(int start, int count)
        {
            return ParallelEnumerable.Range(start, count).Count(n =>
               Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)
            );
        }

        private static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>  ParallelEnumerable.Range(start, count).Count(n =>
               Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)
            ));
        }

        private static void DisplayPrimeCounts()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(GetPrimesCount((i * 1000000) + 2, 1000000) + " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
                Console.WriteLine("Done!");
            }
        }

        private static async Task DisplayPrimeCountsAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(await GetPrimesCountAsync((i * 1000000) + 2, 1000000) + " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
                Console.WriteLine("Done!");
            }
        }
    }
}
