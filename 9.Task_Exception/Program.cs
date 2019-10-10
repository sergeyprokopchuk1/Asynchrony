using System;
using System.Threading.Tasks;

namespace _9.Task_Exception
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Task.Run(() => throw null);

            try
            {
                if(task.IsFaulted)
                {
                    var ex = task.Exception;

                    if (ex.InnerException is NullReferenceException)
                        Console.WriteLine("NullReferenceException inner");
                }
                task.Wait();
            }
            catch(AggregateException ex)
            {
                if (ex.InnerException is NullReferenceException)
                    Console.WriteLine("NullReferenceException 1");
                else
                    throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("NullReferenceException 2");
            }
        }
    }
}
