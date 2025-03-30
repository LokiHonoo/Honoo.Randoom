using Honoo.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Randoom
{
    internal class Program
    {
        private const string MASK_DESCRIPTION =
            "调用 Randoom.NextString(string mask) 返回一个由掩码定义的随机字符串。\r\n" +
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
            "'!' 叹号控制符后一个字符直接输出。在 '{xx!}xx}' 中可用于输出后括号 '}'。\r\n" +
            "'+' 加号控制符之后的随机字符转换为大写形式。不影响直接输出控制符 '{xxx}'。\r\n" +
            "'-' 减号控制符之后的随机字符转换为小写形式。不影响直接输出控制符 '{xxx}'。\r\n" +
            "'.' 点号控制符之后的随机字符不再进行大小写转换。\r\n" +
            "\r\n" +
            "实例：\r\n" +
            "+mmmmm{-}mmmmm{-}mmmmm{-}mmmmm{-}mmmmm            模拟 Windows 序列号。\r\n" +
            "h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12]                 模拟 GUID。\r\n" +
            "{WPD888-5}DDDD{-}DDDDD{-}DDDDD                    模拟 Macromedia 8 序列号。\r\n" +
            "ccccccccccccccccccccccccc@ABCabc12345~!@#$%^*     自定义字符。";

        private static readonly Dictionary<char, Tuple<string, string>> _masks = [];
        private static int _maskPadding = 0;
        private static byte[]? _seed;

        #region Main

        private static void Main()
        {
            if (!File.Exists("mask.xml"))
            {
                using (var manager = CreateMaskStore())
                {
                    LoadMaskStore(manager);
                }
            }
            else
            {
                using (var manager = new XConfigManager("mask.xml"))
                {
                    LoadMaskStore(manager);
                }
            }
            //
            string[] choices =
            [
                "阿拉伯数字",
                "阿拉伯数字，不包括字形易混淆的字符",
                "大写和小写英文字母",
                "大写和小写英文字母，不包括字形易混淆的字符",
                "大写和小写英文字母和阿拉伯数字",
                "大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符",
                "小写十六进制字符",
                "掩码模式",
                "字节数组"
            ];
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
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
                Console.Write($"  6. {choices[5]}"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(" （默认）"); Console.ResetColor();
                Console.WriteLine($"  7. {choices[6]}");
                Console.WriteLine($"  8. {choices[7]}");
                Console.WriteLine($"  9. {choices[8]}");
                Console.WriteLine();
                Console.WriteLine("  S. 生成真随机种子");
                Console.WriteLine("  D. 掩码模式说明");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("按键选择项目 （回车选择默认项）：");
                while (true)
                {
                    var kc = Console.ReadKey(true).KeyChar;
                    switch (kc)
                    {
                        case '1': CreateByToken(choices[0], 'D'); break;
                        case '2': CreateByToken(choices[1], 'd'); break;
                        case '3': CreateByToken(choices[2], 'A'); break;
                        case '4': CreateByToken(choices[3], 'a'); break;
                        case '5': CreateByToken(choices[4], 'M'); break;
                        case '6': case '\r': CreateByToken(choices[5], 'm'); break;
                        case '7': CreateByToken(choices[6], 'h'); break;
                        case '8': CreateByMask(choices[7]); break;
                        case '9': CreateByteArray(choices[8]); break;
                        case 'S': case 's': _seed = BuildSeed(); break;
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

        #region Token

        private static void CreateByToken(string choice, char token)
        {
            int charCount = GetCharCount($"字符标记： {choice}", "字符个数：", "生成数量：");
            if (charCount > 0)
            {
                int createCount = GetCreateCount(4, $"字符标记： {choice}", $"字符个数： {charCount}", "生成数量：");
                var sb = new StringBuilder();
                using (var randoom = new Honoo.Randoom(_seed))
                {
                    for (int i = 0; i < createCount; i++)
                    {
                        sb.AppendLine(randoom.NextString(charCount, token));
                    }
                }
                Finish(sb.ToString(), $"字符标记： {choice}", $"字符个数： {charCount}", $"生成数量： {createCount}");
            }
        }

        private static int GetCharCount(params string[] explain)
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
                foreach (var str in explain)
                {
                    Console.WriteLine($"  {str}");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("输入要生成的字符个数，按回车确认 （直接回车选择"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(" 16 "); Console.ResetColor(); Console.Write("个字符）：");
                string? input = Console.ReadLine();
                if (input == "")
                {
                    return 16;
                }
                if (int.TryParse(input, out int charCount))
                {
                    if (charCount > 0)
                    {
                        return charCount;
                    }
                }
            }
        }

        #endregion Token

        #region ByteArray

        private static void CreateByteArray(string choice)
        {
            int arrayLength = GetByteArrayLength();
            if (arrayLength > 0)
            {
                int createCount = GetCreateCount(2, $"选择类型： {choice}", $"数组长度： {arrayLength} 字节", "生成数量：");
                var sb = new StringBuilder();
                using (var randoom = new Honoo.Randoom(_seed))
                {
                    byte[] buffer = new byte[arrayLength];
                    for (int i = 0; i < createCount; i++)
                    {
                        sb.AppendLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                        randoom.NextNonZeroBytes(buffer);
                        string str = BitConverter.ToString(buffer);
                        sb.AppendLine(str);
                        sb.AppendLine(str.Replace("-", ""));
                        sb.AppendLine("byte[] bytes = new byte[] { 0x" + str.Replace("-", ", 0x") + " };");
                        sb.AppendLine(Convert.ToBase64String(buffer));
                        if (i < createCount - 1)
                        {
                            sb.AppendLine();
                        }
                    }
                }
                Finish(sb.ToString(), $"选择类型： {choice}", $"数组长度： {arrayLength} 字节", $"生成数量： {createCount}");
            }
        }

        private static int GetByteArrayLength()
        {
            Console.Clear();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
            Console.WriteLine();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("  字节数组可用于对称加密算法、HMAC、密钥交换随机数等");
            Console.WriteLine();
            Console.WriteLine("  1. 64 位 8 字节");
            Console.Write("  2. 128 位 16 字节"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(" （默认）"); Console.ResetColor();
            Console.WriteLine("  3. 192 位 24 字节");
            Console.WriteLine("  4. 256 位 32 字节");
            Console.WriteLine("  5. 512 位 64 字节");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("按键选择项目 （回车选择默认项）：");
            while (true)
            {
                var kc = Console.ReadKey(true).KeyChar;
                switch (kc)
                {
                    case '1': return 8;
                    case '2': case '\r': Console.WriteLine(); return 16;
                    case '3': return 24;
                    case '4': return 32;
                    case '5': return 64;
                    default: continue;
                }
            }
        }

        #endregion ByteArray

        #region Mask

        private static void CreateByMask(string choice)
        {
            string mask = GetMask();
            if (!string.IsNullOrWhiteSpace(mask))
            {
                int createCount = GetCreateCount(4, $"选择类型： {choice}", $"掩码文本： {mask}", "生成数量：");
                var sb = new StringBuilder();
                using (var randoom = new Honoo.Randoom(_seed))
                {
                    for (int i = 0; i < createCount; i++)
                    {
                        sb.AppendLine(randoom.NextString(mask));
                    }
                }
                Finish(sb.ToString(), $"选择类型： {choice}", $"掩码文本： {mask}", $"生成数量： {createCount}");
            }
        }

        private static XConfigManager CreateMaskStore()
        {
            var manager = new XConfigManager();
            manager.Default.Comment.SetValue("\r\n" + MASK_DESCRIPTION + "\r\n");
            manager.Default.Properties.Add("1", new XString("+mmmmm{-}mmmmm{-}mmmmm{-}mmmmm{-}mmmmm")).Attributes.Add("description", new XConfigAttribute("模拟 Windows 序列号"));
            manager.Default.Properties.Add("2", new XString("h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12]")).Attributes.Add("description", new XConfigAttribute("模拟 GUID"));
            manager.Default.Properties.Add("3", new XString("{WPD888-5}DDDD{-}DDDDD{-}DDDDD")).Attributes.Add("description", new XConfigAttribute("模拟 Macromedia 8 序列号"));
            manager.Default.Properties.Add("4", new XString("ccccccccccccccccccccccccc@ABCabc12345~!@#$%^*")).Attributes.Add("description", new XConfigAttribute("自定义字符"));
            manager.Save("mask.xml");
            return manager;
        }

        private static string GetMask()
        {
            Console.Clear();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
            Console.WriteLine();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("  使用掩码生成随机字符串（编辑 mask.xml 增加掩码模板）");
            Console.WriteLine();
            foreach (var mask in _masks)
            {
                string str = $"  {mask.Key}. {mask.Value.Item1}".PadRight(_maskPadding, ' ');
                Console.Write(str); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($" {mask.Value.Item2}"); Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine("  Z. 返回主菜单");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("按键选择项目：");
            while (true)
            {
                var kc = Console.ReadKey(true).KeyChar;
                if (kc == 'Z' || kc == 'z')
                {
                    Console.WriteLine();
                    return string.Empty;
                }
                if (_masks.TryGetValue(kc, out Tuple<string, string>? value))
                {
                    return value.Item1;
                }
            }
        }

        private static void LoadMaskStore(XConfigManager manager)
        {
            foreach (var kv in manager.Default.Properties)
            {
                XString property = (XString)kv.Value;
                char pr = char.Parse(kv.Key);
                string mask = property.GetStringValue();
                string des = property.Attributes.GetValue("description", new XConfigAttribute(string.Empty)).GetStringValue();
                _maskPadding = Math.Max(_maskPadding, mask.Length);
                _masks.Add(pr, new Tuple<string, string>(mask, des));
            }
            _maskPadding += 9;
        }

        #endregion Mask

        private static byte[] BuildSeed()
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
            Console.WriteLine("  生成的随机数种子在当前程序域使用");
            Console.WriteLine();
            Console.WriteLine("  0/256");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("按任意键获取随机事件，回车键终止：");
            while (true)
            {
                var kc = Console.ReadKey(true).KeyChar;
                if (kc == '\r')
                {
                    Console.SetCursorPosition(34, 11);
                    Console.WriteLine();
                    return seed.ToArray();
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
                    Console.SetCursorPosition(2, 8);
                    Console.Write("{0}/256", count);
                }
            }
        }

        private static void Finish(string result, params string[] explain)
        {
            Console.Clear();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            Console.WriteLine("                            Randoom creater   runtime " + Environment.Version);
            Console.WriteLine();
            Console.WriteLine("=======================================================================================");
            Console.WriteLine();
            foreach (var str in explain)
            {
                Console.WriteLine($"  {str}");
            }
            Console.WriteLine();
            Console.WriteLine(result);
        }

        private static int GetCreateCount(int defaultCount, params string[] explain)
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
                foreach (var str in explain)
                {
                    Console.WriteLine($"  {str}");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("输入要生成的数量，按回车确认 （直接回车选择"); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write($" {defaultCount} "); Console.ResetColor(); Console.Write("条）：");
                string? input = Console.ReadLine();
                if (input == "")
                {
                    return defaultCount;
                }
                if (int.TryParse(input, out int charCount))
                {
                    if (charCount > 0)
                    {
                        return charCount;
                    }
                }
            }
        }
    }
}