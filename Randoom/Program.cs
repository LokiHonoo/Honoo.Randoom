using Honoo.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Randoom
{
    internal class Program
    {
        private const string MASK_DESCRIPTION =
            "调用 NextString(string mask) 返回一个由掩码定义的随机字符串。\r\n" +
            "\r\n" +
            "字符范围标记和控制符：\r\n" +
            "'D' 或 '0' 阿拉伯数字。\r\n" +
            "'d' 或 '1' 阿拉伯数字，不包括字形易混淆的字符。\r\n" +
            "'A' 大写和小写英文字母。\r\n" +
            "'a' 大写和小写英文字母，不包括字形易混淆的字符。\r\n" +
            "'M' 大写和小写英文字母和阿拉伯数字。\r\n" +
            "'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。\r\n" +
            "'h' 小写十六进制字符。\r\n" +
            "'c' 使用自定义字符集合。需配合 '@' 控制符同时使用。\r\n" +
            "'[number]' 方括号控制符之内的数字表示输出前一随机字符的个数。\r\n" +
            "'@' 控制符之后的字符作为自定义字符。需配合 'c' 标记同时使用。\r\n" +
            "'{xxx}' 大括号控制符之内的字符直接输出，不作为掩码字符。\r\n" +
            "'!' 叹号控制符后一个字符直接输出。在 '{xx!xx}' 中可用于输出后括号 '}'。\r\n" +
            "'+' 加号控制符之后的随机字符转换为大写形式。不影响直接输出控制符 '{xxx}'。\r\n" +
            "'-' 减号控制符之后的随机字符转换为小写形式。不影响直接输出控制符 '{xxx}'。\r\n" +
            "'.' 点号控制符之后的随机字符不再进行大小写转换。\r\n" +
            "\r\n" +
            "实例：\r\n" +
            "+mmmmm{-}mmmmm{-}mmmmm{-}mmmmm{-}mmmmm ||| 模拟 Windows 序列号。\r\n" +
            "h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12] ||| 模拟 GUID。\r\n" +
            "{WPD888-5}DDDD{-}DDDDD{-}DDDDD ||| 模拟 Macromedia 8 序列号。\r\n" +
            "ccccccccccccccccccccccccc@ABCabc12345~!@#$%^* ||| 自定义字符。";

        private static byte[]? _seed;

        #region Main

        private static void Main()
        {
            string[] choices =
            [
                "阿拉伯数字",
                "阿拉伯数字，不包括字形易混淆的字符",
                "大写和小写英文字母",
                "大写和小写英文字母，不包括字形易混淆的字符",
                "大写和小写英文字母和阿拉伯数字",
                "大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符",
                "小写十六进制字符",
                "掩码模式（编辑 mask.xml 增加掩码模板）",
                "字节数组"
            ];
            int charCount;
            string mask;
            int byteCount;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine($"  1. {choices[0]}");
                Console.WriteLine($"  2. {choices[1]}");
                Console.WriteLine($"  3. {choices[2]}");
                Console.WriteLine($"  4. {choices[3]}");
                Console.WriteLine($"  5. {choices[4]}");
                Console.WriteLine($"  6. {choices[5]}");
                Console.WriteLine($"  7. {choices[6]}");
                Console.WriteLine($"  8. {choices[7]}");
                Console.WriteLine($"  9. {choices[8]}");
                Console.WriteLine();
                Console.WriteLine("  S. 生成真随机种子");
                Console.WriteLine("  D. 掩码说明");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("按键选择项目:");
                while (true)
                {
                    var kc = Console.ReadKey(true).KeyChar;
                    switch (kc)
                    {
                        case '1': charCount = GetCharCount(choices[0]); if (charCount > 0) { Create(choices[0], charCount, 'D', GetCount(choices[0], charCount)); } break;
                        case '2': charCount = GetCharCount(choices[1]); if (charCount > 0) { Create(choices[1], charCount, 'd', GetCount(choices[1], charCount)); } break;
                        case '3': charCount = GetCharCount(choices[2]); if (charCount > 0) { Create(choices[2], charCount, 'A', GetCount(choices[2], charCount)); } break;
                        case '4': charCount = GetCharCount(choices[3]); if (charCount > 0) { Create(choices[3], charCount, 'a', GetCount(choices[3], charCount)); } break;
                        case '5': charCount = GetCharCount(choices[4]); if (charCount > 0) { Create(choices[4], charCount, 'M', GetCount(choices[4], charCount)); } break;
                        case '6': charCount = GetCharCount(choices[5]); if (charCount > 0) { Create(choices[5], charCount, 'm', GetCount(choices[5], charCount)); } break;
                        case '7': charCount = GetCharCount(choices[6]); if (charCount > 0) { Create(choices[6], charCount, 'h', GetCount(choices[6], charCount)); } break;
                        case '8': mask = GetMask(choices[7]); Create(choices[7], mask, GetCount(choices[7], mask)); break;
                        case '9': byteCount = GetByteCount(); Create(choices[8], byteCount); break;
                        case 'S': case 's': BuildSeed(); break;
                        case 'D': case 'd': Console.Clear(); Console.WriteLine(MASK_DESCRIPTION); break;
                        default: continue;
                    }
                    break;
                }
                Console.WriteLine();
                Console.Write("按任意键返回主菜单...");
                Console.ReadKey(true);
            }
        }

        #endregion Main

        private static void BuildSeed()
        {
            var seed = new List<byte>();
            int count = 0;
            Console.Clear();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
            Console.WriteLine();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("  0/256");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("按任意按键获取随机事件,回车键终止:");
            while (true)
            {
                var kc = Console.ReadKey(true).KeyChar;
                if (kc == '\r')
                {
                    _seed = seed.ToArray();
                    Console.SetCursorPosition(34, 9);
                    Console.WriteLine();
                    break;
                }
                else
                {
                    DateTime time = DateTime.Now;
                    long v = time.Ticks ^ kc ^ 0xFF;
                    seed.Add((byte)v);
                    seed.Add((byte)(v >> 8));
                    seed.Add((byte)(v >> 16));
                    seed.Add((byte)(v >> 24));
                    count += 4;
                    Console.SetCursorPosition(2, 6);
                    Console.Write("{0}/256", count);
                }
            }
        }

        private static void Create(string choice, int byteCount)
        {
            Console.Clear();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
            Console.WriteLine();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine($"  字符选择 - {choice}");
            Console.WriteLine();
            Console.WriteLine();
            using (var randoom = new Honoo.Randoom(_seed))
            {
                byte[] buffer = new byte[byteCount];
                randoom.NextNonZeroBytes(buffer);
                string str = BitConverter.ToString(buffer);
                Console.WriteLine(str);
                Console.WriteLine();
                Console.WriteLine(str.Replace("-", ""));
                Console.WriteLine();
                Console.WriteLine("byte[] bytes = new byte[] { 0x" + str.Replace("-", ", 0x") + " };");
                Console.WriteLine();
                Console.WriteLine(Convert.ToBase64String(buffer));
            }
        }

        private static void Create(string choice, int charCount, char token, int count)
        {
            Console.Clear();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
            Console.WriteLine();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine($"  字符选择 - {choice}");
            Console.WriteLine($"  字符数量 - {charCount}");
            Console.WriteLine($"  生成数量 - {count}");
            Console.WriteLine();
            Console.WriteLine();
            using (var randoom = new Honoo.Randoom(_seed))
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(randoom.NextString(charCount, token));
                }
            }
        }

        private static void Create(string choice, string mask, int count)
        {
            Console.Clear();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
            Console.WriteLine();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine($"  字符选择 - {choice}");
            Console.WriteLine($"  掩码文本 - {mask}");
            Console.WriteLine($"  生成数量 - {count}");
            Console.WriteLine();
            Console.WriteLine();
            using (var randoom = new Honoo.Randoom(_seed))
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(randoom.NextString(mask));
                }
            }
        }

        private static int GetByteCount()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine($"  对称加密算法/HMAC/密钥交换随机数");
                Console.WriteLine();
                Console.WriteLine("  1. 64 bit - 8 bytes");
                Console.WriteLine("  2. 128 bit - 16 bytes");
                Console.WriteLine("  3. 192 bit - 24 bytes");
                Console.WriteLine("  4. 256 bit - 32 bytes");
                Console.WriteLine("  5. 512 bit - 64 bytes");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("按键选择项目:");
                while (true)
                {
                    var kc = Console.ReadKey(true).KeyChar;
                    switch (kc)
                    {
                        case '1': return 8;
                        case '2': return 16;
                        case '3': return 24;
                        case '4': return 32;
                        case '5': return 64;
                        default: continue;
                    }
                }
            }
        }

        private static int GetCharCount(string choice)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine($"  字符选择 - {choice}");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("输入要生成字符串的字符数量，按回车确认:");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int charCount))
                {
                    return charCount;
                }
            }
        }

        private static int GetCount(string choice, int charCount)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine($"  字符选择 - {choice}");
                Console.WriteLine($"  字符数量 - {charCount}");
                Console.WriteLine();
                Console.WriteLine("  1. 生成数量 - 4");
                Console.WriteLine("  2. 生成数量 - 8");
                Console.WriteLine("  3. 生成数量 - 16");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("按键选择项目:");
                while (true)
                {
                    var kc = Console.ReadKey(true).KeyChar;
                    switch (kc)
                    {
                        case '1': return 4;
                        case '2': return 8;
                        case '3': return 16;
                        default: continue;
                    }
                }
            }
        }

        private static int GetCount(string choice, string mask)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine($"  字符选择 - {choice}");
                Console.WriteLine($"  掩码文本 - {mask}");
                Console.WriteLine();
                Console.WriteLine("  1. 生成数量 - 4");
                Console.WriteLine("  2. 生成数量 - 8");
                Console.WriteLine("  3. 生成数量 - 16");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("按键选择项目:");
                while (true)
                {
                    var kc = Console.ReadKey(true).KeyChar;
                    switch (kc)
                    {
                        case '1': return 4;
                        case '2': return 8;
                        case '3': return 16;
                        default: continue;
                    }
                }
            }
        }

        private static string GetMask(string choice)
        {
            while (true)
            {
                var choices = new List<Tuple<char, string, string>>();
                if (File.Exists("mask.xml"))
                {
                    using (var manager = new XConfigManager("mask.xml"))
                    {
                        foreach (var kv in manager.Default.Properties)
                        {
                            XString property = (XString)kv.Value;
                            char pr = char.Parse(kv.Key);
                            string mask = property.GetStringValue();
                            string des = property.Attributes.GetValue("description", new XConfigAttribute(string.Empty)).GetStringValue();
                            choices.Add(new Tuple<char, string, string>(pr, mask, mask + " ||| " + des));
                        }
                    }
                }
                else
                {
                    using (var manager = new XConfigManager())
                    {
                        manager.Default.Comment.SetValue("\r\n" + MASK_DESCRIPTION + "\r\n");
                        manager.Default.Properties.Add("1", new XString("+mmmmm{-}mmmmm{-}mmmmm{-}mmmmm{-}mmmmm")).Attributes.Add("description", new XConfigAttribute("模拟 Windows 序列号"));
                        choices.Add(new Tuple<char, string, string>('1', "+mmmmm{-}mmmmm{-}mmmmm{-}mmmmm{-}mmmmm", "+mmmmm{-}mmmmm{-}mmmmm{-}mmmmm{-}mmmmm ||| 模拟 Windows 序列号"));
                        manager.Default.Properties.Add("2", new XString("h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12]")).Attributes.Add("description", new XConfigAttribute("模拟 GUID"));
                        choices.Add(new Tuple<char, string, string>('2', "h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12]", "h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12] ||| 模拟 GUID"));
                        manager.Default.Properties.Add("3", new XString("{WPD888-5}DDDD{-}DDDDD{-}DDDDD")).Attributes.Add("description", new XConfigAttribute("模拟 Macromedia 8 序列号"));
                        choices.Add(new Tuple<char, string, string>('3', "{WPD888-5}DDDD{-}DDDDD{-}DDDDD", "{WPD888-5}DDDD{-}DDDDD{-}DDDDD ||| 模拟 Macromedia 8 序列号"));
                        manager.Default.Properties.Add("4", new XString("{ccccccccccccccccccccccccc@ABCabc12345~!@#$%^*")).Attributes.Add("description", new XConfigAttribute("自定义字符")); ;
                        choices.Add(new Tuple<char, string, string>('4', "ccccccccccccccccccccccccc@ABCabc12345~!@#$%^*", "ccccccccccccccccccccccccc@ABCabc12345~!@#$%^* ||| 自定义字符"));
                        manager.Save("mask.xml");
                    }
                }

                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine($"  字符选择 - {choice}");
                Console.WriteLine();
                Console.WriteLine();
                foreach (var item in choices)
                {
                    Console.WriteLine($"  {item.Item1}. {item.Item3}");
                }
                Console.WriteLine();
                Console.Write("按键选择项目:");
                while (true)
                {
                    var kc = Console.ReadKey(true).KeyChar;
                    foreach (var item in choices)
                    {
                        if (kc == item.Item1)
                        {
                            return item.Item2;
                        }
                    }
                }
            }
        }
    }
}