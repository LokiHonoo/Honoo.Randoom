using Honoo;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

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
                Randoom randoom = new Randoom(SHA256.Create());
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
                    Console.WriteLine(randoom.Next(-30, 30));
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
                Console.WriteLine("Randoom.NextString(\"+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm\")");
                Console.WriteLine(randoom.NextString("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm"));
                Console.WriteLine(randoom.NextString("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm"));
                Console.WriteLine(randoom.NextString("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"hhhhhhhh(-)hhhh(-)hhhh(-)hhhh(-)hhhhhhhhhhhh\")");
                Console.WriteLine(randoom.NextString("hhhhhhhh(-)hhhh(-)hhhh(-)hhhh(-)hhhhhhhhhhhh"));
                Console.WriteLine(randoom.NextString("hhhhhhhh(-)hhhh(-)hhhh(-)hhhh(-)hhhhhhhhhhhh"));
                Console.WriteLine(randoom.NextString("hhhhhhhh(-)hhhh(-)hhhh(-)hhhh(-)hhhhhhhhhhhh"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"(WPD888-5)DDDD(-)DDDDD(-)DDDDD\")");
                Console.WriteLine(randoom.NextString("(WPD888-5)DDDD(-)DDDDD(-)DDDDD"));
                Console.WriteLine(randoom.NextString("(WPD888-5)DDDD(-)DDDDD(-)DDDDD"));
                Console.WriteLine(randoom.NextString("(WPD888-5)DDDD(-)DDDDD(-)DDDDD"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*\")");
                Console.WriteLine(randoom.NextString("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*"));
                Console.WriteLine(randoom.NextString("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*"));
                Console.WriteLine(randoom.NextString("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*"));
                Console.WriteLine();
                char[] mixture = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                Dictionary<char, int[]> sta = new Dictionary<char, int[]>();
                foreach (var m in mixture)
                {
                    sta.Add(m, new int[1]);
                }
                for (int i = 0; i < 1000; i++)
                {
                    string r = randoom.NextString('M', 16);
                    foreach (var c in r)
                    {
                        sta[c][0] += 1;
                    }
                }
                foreach (var item in sta)
                {
                    Console.WriteLine(item.Key + ": " + item.Value[0]);
                }

                Console.ReadKey(true);
            }
        }
    }
}