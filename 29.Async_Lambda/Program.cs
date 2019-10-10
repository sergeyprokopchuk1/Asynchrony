using System;
using System.Threading.Tasks;

namespace _29.Async_Lambda
{
    class Program
    {
        private static event EventHandler<EventArgs> EventStatic;

        static async Task Main(string[] args)
        {
            //1. named method
            await NamedMethod();

            //2. unnamed method
            Func<Task> unnamed = async () => 
            {
                await Task.Delay(1000);
                Console.WriteLine("Unnamed Method");
            };

            await unnamed();

            //3. Unnamed method with result
            Func<Task<int>> unnamedResult = async () => 
            {
                await Task.Delay(1000);
                return 1;
            };

            Console.WriteLine($"Unnamed Method with result - {await unnamedResult()}");

            //4. async event
            EventStatic += async (sender, args) =>
            {
                await Task.Delay(1000);
                Console.WriteLine("Event");
            };

            EventStatic(null, null);
            EventStatic.Invoke(null, null);

            Console.ReadKey();
        }

        private static async Task NamedMethod()
        {
            await Task.Delay(1000);
            Console.WriteLine("Named Method.");
        }
    }
}
