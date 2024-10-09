using Honoo;
using System;
using System.Collections.Generic;

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

                var randoom = new Randoom();
                //
                Console.WriteLine("randoom.NextDouble()");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextDouble());
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.Next()");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.Next());
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.Next(900000000)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.Next(900000000));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.Next(-900000000, 900000000)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.Next(-900000000, 900000000));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.Next(-50, 50)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.Next(-50, 50));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextBytes(bytes)");
                for (int i = 0; i < 10; i++)
                {
                    byte[] bytes = new byte[20];
                    randoom.NextBytes(bytes);
                    Console.WriteLine(BitConverter.ToString(bytes));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextNonZeroBytes(bytes)");
                for (int i = 0; i < 10; i++)
                {
                    byte[] bytes = new byte[20];
                    randoom.NextNonZeroBytes(bytes);
                    Console.WriteLine(BitConverter.ToString(bytes));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextSingle()");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextSingle());
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextInt64()");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextInt64());
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextInt64(9000000000000000000)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextInt64(9000000000000000000));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextInt64(-9000000000000000000, 9000000000000000000)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextInt64(-9000000000000000000, 9000000000000000000));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextInt64(-50, 50)");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextInt64(-50, 50));
                }
                Console.WriteLine();

#if NET6_0_OR_GREATER
                //
                Console.WriteLine("randoom.NextBytes(spanbytes)");
                for (int i = 0; i < 10; i++)
                {
                   Span<byte>  bytes = new byte[20];
                    randoom.NextBytes(bytes);
                    Console.WriteLine(BitConverter.ToString(bytes.ToArray()));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextNonZeroBytes(spanbytes)");
                for (int i = 0; i < 10; i++)
                {
                    Span<byte> bytes = new byte[20];
                    randoom.NextNonZeroBytes(bytes);
                    Console.WriteLine(BitConverter.ToString(bytes.ToArray()));
                }
                Console.WriteLine();

#endif
                //
                Console.WriteLine("randoom.NextString(60, 'm')");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextString(60, 'm'));
                }
                Console.WriteLine();
                //
                Console.WriteLine("randoom.NextString(60, \"!@#$%^&*()_~!@#$%^&*()^&*()_+\")");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(randoom.NextString(60, "!@#$%^&*()_~!@#$%^&*()^&*()_+"));
                }
                Console.WriteLine();
                //
                Console.WriteLine("Randoom.NextString(\"+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm\")");
                Console.WriteLine(randoom.NextString("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm"));
                Console.WriteLine(randoom.NextString("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm"));
                Console.WriteLine(randoom.NextString("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"h[8](-)h[4](-)h[4](-)h[4](-)h[12]\")");
                Console.WriteLine(randoom.NextString("h[8](-)h[4](-)h[4](-)h[4](-)h[12]"));
                Console.WriteLine(randoom.NextString("h[8](-)h[4](-)h[4](-)h[4](-)h[12]"));
                Console.WriteLine(randoom.NextString("h[8](-)h[4](-)h[4](-)h[4](-)h[12]"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"(WPD888-5)DDDD(-)DDDDD(-)DDDDD\")");
                Console.WriteLine(randoom.NextString("(WPD888-5)DDDD(-)DDDDD(-)DDDDD"));
                Console.WriteLine(randoom.NextString("(WPD888-5)DDDD(-)DDDDD(-)DDDDD"));
                Console.WriteLine(randoom.NextString("(WPD888-5)DDDD(-)DDDDD(-)DDDDD"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"(TB(667!)ED-)aaaaaa!-aaaaaaaa\")");
                Console.WriteLine(randoom.NextString("(TB(667!)ED-)aaaaaa!-aaaaaaaa"));
                Console.WriteLine(randoom.NextString("(TB(667!)ED-)aaaaaa!-aaaaaaaa"));
                Console.WriteLine(randoom.NextString("(TB(667!)ED-)aaaaaa!-aaaaaaaa"));
                Console.WriteLine();
                Console.WriteLine("Randoom.NextString(\"(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*\")");
                Console.WriteLine(randoom.NextString("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*"));
                Console.WriteLine(randoom.NextString("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*"));
                Console.WriteLine(randoom.NextString("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*"));
                Console.WriteLine();
                //
                var sta1 = new Dictionary<int, int[]>();
                int b = int.MaxValue / 10;
                for (int i = 1; i <= 10; i++)
                {
                    sta1.Add(b * i, new int[1]);
                }
                for (int i = 0; i < 1000000; i++)
                {
                    int r = randoom.Next();
                    foreach (var item in sta1)
                    {
                        if (r <= item.Key)
                        {
                            item.Value[0] += 1;
                            break;
                        }
                    }
                }
                Console.WriteLine("1000000 int distributions: ");
                foreach (var item in sta1)
                {
                    Console.WriteLine(("Rank " + item.Key + ": ").PadRight(18, ' ') + item.Value[0]);
                }
                Console.WriteLine();
                var sta2 = new Dictionary<char, int[]>();
                char[] mixture = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                foreach (var m in mixture)
                {
                    sta2.Add(m, new int[1]);
                }
                for (int i = 0; i < 1000; i++)
                {
                    string r = randoom.NextString(16, 'M');
                    foreach (var c in r)
                    {
                        sta2[c][0] += 1;
                    }
                }
                Console.WriteLine("16000 char distributions: ");
                foreach (var item in sta2)
                {
                    Console.WriteLine(item.Key + ": " + item.Value[0]);
                }
                Console.WriteLine();
                //
                int c1 = 0;
                int c2 = 0;
                int c3 = 0;
                Console.WriteLine("randoom.Next(-900000000, 900000000)");
                for (int i = 0; i < 100; i++)
                {
                    int v = randoom.Next(-900000000, 900000000);
                    if (v < 0)
                    {
                        c1++;
                    }
                    if (v >= 0)
                    {
                        c2++;
                    }
                    if (v < -900000000 || v > 900000000)
                    {
                        c3++;
                    }
                }
                Console.WriteLine($"less0={c1}    more0={c2}   overflow={c3}");
                Console.WriteLine();
                //
                c1 = 0;
                c2 = 0;
                c3 = 0;
                Console.WriteLine("randoom.NextInt64(-9000000000000000000, 9000000000000000000)");
                for (int i = 0; i < 100; i++)
                {
                    long v = randoom.NextInt64(-9000000000000000000, 9000000000000000000);
                    if (v < 0)
                    {
                        c1++;
                    }
                    if (v >= 0)
                    {
                        c2++;
                    }
                    if (v < -9000000000000000000 || v > 9000000000000000000)
                    {
                        c3++;
                    }
                }
                Console.WriteLine($"less0={c1}    more0={c2}   overflow={c3}");
                Console.WriteLine();
                //
                //
                //
                Console.ReadKey(true);
            }
        }
    }
}