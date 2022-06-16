using Honoo;
using System;
using System.Diagnostics;

namespace test
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                       Honoo.Randoom TEST   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");

                Random random = new Random();
                Randoom randoom = new Randoom();
                Console.WriteLine("Randoom.NextDouble()");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextDouble());
                }
                Console.WriteLine();
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(random.NextDouble());
                }
                Console.WriteLine();
                Console.WriteLine("Randoom.Next()");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.Next());
                }
                Console.WriteLine();
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(random.Next());
                }
                Console.WriteLine();
                Console.WriteLine("Randoom.Next(90000)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.Next(90000));
                }
                Console.WriteLine();
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(random.Next(90000));
                }
                Console.WriteLine();
                Console.WriteLine("Randoom.Next(-30,30)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.Next(-30,30));
                }
                Console.WriteLine();
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(random.Next(-30, 30));
                }
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString('m',60)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextString('M', 60));
                }
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"+mmmmm!-!mmmmm!-!mmmmm!-!mmmmm!-!mmmmm\")");
                Console.WriteLine(randoom.NextString("+mmmmm!-!mmmmm!-!mmmmm!-!mmmmm!-!mmmmm"));
                Console.WriteLine(randoom.NextString("+mmmmm!-!mmmmm!-!mmmmm!-!mmmmm!-!mmmmm"));
                Console.WriteLine(randoom.NextString("+mmmmm!-!mmmmm!-!mmmmm!-!mmmmm!-!mmmmm"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"hhhhhhhh!-!hhhh!-!hhhh!-!hhhh!-!hhhhhhhhhhhh\")");
                Console.WriteLine(randoom.NextString("hhhhhhhh!-!hhhh!-!hhhh!-!hhhh!-!hhhhhhhhhhhh"));
                Console.WriteLine(randoom.NextString("hhhhhhhh!-!hhhh!-!hhhh!-!hhhh!-!hhhhhhhhhhhh"));
                Console.WriteLine(randoom.NextString("hhhhhhhh!-!hhhh!-!hhhh!-!hhhh!-!hhhhhhhhhhhh"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"!WPD888-5!DDDD!-!DDDDD!-!DDDDD\")");
                Console.WriteLine(randoom.NextString("!WPD888-5!DDDD!-!DDDDD!-!DDDDD"));
                Console.WriteLine(randoom.NextString("!WPD888-5!DDDD!-!DDDDD!-!DDDDD"));
                Console.WriteLine(randoom.NextString("!WPD888-5!DDDD!-!DDDDD!-!DDDDD"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"cccccccccccccccccccccccc@ABCabc12345~!@#$%^*\")");
                Console.WriteLine(randoom.NextString("cccccccccccccccccccccccc@ABCabc12345~!@#$%^*"));
                Console.WriteLine(randoom.NextString("cccccccccccccccccccccccc@ABCabc12345~!@#$%^*"));
                Console.WriteLine(randoom.NextString("cccccccccccccccccccccccc@ABCabc12345~!@#$%^*"));
                Console.WriteLine();
                Console.ReadKey(true);
            }



        }
    }
}