using System;
using System.Threading.Tasks;

namespace _30.Async_Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            //unhandled exception
            try
            {
                Method();
            }
            catch(Exception e)
            {
                var ex = e;
                Console.WriteLine($"Exception = {ex.Message}");
            }

            Console.ReadKey();
        }

        private static async void Method()
        {
            throw null;

            await Task.Delay(1000);
        }
    }
}
