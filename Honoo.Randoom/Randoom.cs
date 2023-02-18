using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Honoo
{
    /// <summary>
    /// 加强型随机值生成器。
    /// </summary>
    public sealed class Randoom : IDisposable
    {
        #region 成员

        /// <summary>大写和小写英文字母。</summary>
        private static readonly char[] _alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        /// <summary>大写和小写英文字母，不包括字形易混淆的字符。</summary>
        private static readonly char[] _alphabetLess = "bcdfghjkmpqrtvwxyBCDFGHJKMPQRTVWXY".ToCharArray();

        /// <summary>阿拉伯数字。</summary>
        private static readonly char[] _digital = "0123456789".ToCharArray();

        /// <summary>阿拉伯数字，不包括字形易混淆的字符。</summary>
        private static readonly char[] _digitalLess = "2346789".ToCharArray();

        /// <summary>小写十六进制字符。</summary>
        private static readonly char[] _hex = "0123456789abcdef".ToCharArray();

        /// <summary>大写和小写英文字母和阿拉伯数字。</summary>
        private static readonly char[] _mixture = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        /// <summary>大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。</summary>
        private static readonly char[] _mixtureLess = "2346789bcdfghjkmpqrtvwxyBCDFGHJKMPQRTVWXY".ToCharArray();

        private bool _disposed;
        private byte[] _hash;
        private HashAlgorithm _hashAlgorithm;
        private IDictionary<char, Room> _rooms;
        private byte[] _tmp = new byte[8];

        #endregion 成员

        #region 构造

        /// <summary>
        /// 创建 Randoom 的新实例。
        /// </summary>
        public Randoom() : this(null, HashAlgorithm.Create("SHA1"))
        {
        }

        /// <summary>
        /// 创建 Randoom 的新实例。
        /// </summary>
        /// <param name="seed">额外的种子，通常采集终端用户的鼠标、按键等行为生成。</param>
        public Randoom(byte[] seed) : this(seed, HashAlgorithm.Create("SHA1"))
        {
        }

        /// <summary>
        /// 创建 Randoom 的新实例。
        /// </summary>
        /// <param name="seed">额外的种子，通常采集终端用户的鼠标、按键等行为生成。</param>
        /// <param name="hashAlgorithm">用于随机数生成的 hash 算法实例。必须是 hash size 64 bits 以上的算法。</param>
        public Randoom(byte[] seed, HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException(nameof(hashAlgorithm));
            byte[] guid = Guid.NewGuid().ToByteArray();
            byte[] buffer;
            if (seed == null || seed.Length == 0)
            {
                buffer = guid;
            }
            else
            {
                buffer = new byte[seed.Length + guid.Length];
                Buffer.BlockCopy(seed, 0, buffer, 0, seed.Length);
                Buffer.BlockCopy(guid, 0, buffer, seed.Length, guid.Length);
            }
            _hash = _hashAlgorithm.ComputeHash(buffer);
            if (_hash.Length < 8)
            {
                throw new CryptographicException("Hash algorithm's hash size must be more than 64 bits.");
            }
        }

        /// <summary>
        /// 释放由 <see cref="Randoom"/> 使用的资源。
        /// </summary>
        ~Randoom()
        {
            Dispose(false);
        }

        /// <summary>
        /// 执行与释放或重置非托管资源关联的的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放由 <see cref="Randoom"/> 使用的非托管资源。
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _hashAlgorithm.Dispose();
                    _hashAlgorithm = null;
                }
                _rooms = null;
                _hash = null;
                _tmp = null;
                _disposed = true;
            }
        }

        #endregion 构造

        /// <summary>
        /// 返回一个非负随机整数。
        /// <para/>返回结果：大于或等于 0 且小于 <see cref="int.MaxValue"/> 的 32 位有符号整数。
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return Next(0, int.MaxValue);
        }

        /// <summary>
        /// 返回一个小于所指定最大值的非负随机整数。
        /// <para/>返回结果：大于或等于 0 且小于 maxValue 的 32 位有符号整数。
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        /// <summary>
        /// 返回在指定范围内的任意整数。
        /// <para/>返回结果：一个大于等于 minValue 且小于 maxValue 的 32 位有符号整数。
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public int Next(int minValue, int maxValue)
        {
            double value = Simple();
            value *= (maxValue - minValue);
            value += minValue;
            if (value < minValue)
            {
                value = minValue;
            }
            if (value >= maxValue)
            {
                value = maxValue - 1;
            }
            return (int)value;
        }

        /// <summary>
        /// 用加强型随机值序列填充字节数组。
        /// </summary>
        /// <param name="buffer"></param>
        public void NextBytes(byte[] buffer)
        {
            NextBytes(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 用加强型随机值序列填充字节数组。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void NextBytes(byte[] buffer, int offset, int length)
        {
            int len;
            while (length > 0)
            {
                len = Math.Min(_hash.Length, length);
                Buffer.BlockCopy(_hash, 0, buffer, offset, len);
                _hash = _hashAlgorithm.ComputeHash(_hash);
                length -= len;
                offset += len;
            }
        }

        /// <summary>
        /// 返回一个大于或等于 0.0 且小于 1.0 的随机浮点数。
        /// <para/>返回结果：大于或等于 0.0 且小于 1.0 的双精度浮点数。
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            return Simple();
        }

        /// <summary>
        /// 用加强型随机非零值序列填充字节数组。
        /// </summary>
        /// <param name="buffer"></param>
        public void NextNonZeroBytes(byte[] buffer)
        {
            NextNonZeroBytes(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 用加强型随机非零值序列填充字节数组。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void NextNonZeroBytes(byte[] buffer, int offset, int length)
        {
            int offsetT;
            while (length > 0)
            {
                offsetT = 0;
                while (length > 0 || offsetT < _hash.Length)
                {
                    if (_hash[offsetT] > 0x00)
                    {
                        buffer[offset] = _hash[offsetT];
                        offset++;
                        length--;
                    }
                    offsetT++;
                }
                _hash = _hashAlgorithm.ComputeHash(_hash);
            }
        }

        private IList<int> Next(int count, int minValue, int maxValue)
        {
            List<double> doubles = new List<double>();
            int offset;
            while (count > 0)
            {
                offset = 0;
                while (count > 0 && offset + 8 < _hash.Length)
                {
                    Buffer.BlockCopy(_hash, offset, _tmp, 0, _tmp.Length);
                    if (BitConverter.IsLittleEndian)
                    {
                        _tmp[6] |= 0xF0;
                        _tmp[7] = 0x3F;
                    }
                    else
                    {
                        _tmp[0] = 0x3F;
                        _tmp[1] |= 0xF0;
                    }
                    double d = BitConverter.ToDouble(_tmp, 0);
                    if (d < 0.1d)
                    {
                        d *= 10d;
                    }
                    if (d >= 1d)
                    {
                        d -= 1d;
                    }
                    doubles.Add(d);
                    offset += 8;
                    count--;
                }
                _hash = _hashAlgorithm.ComputeHash(_hash);
            }
            //
            List<int> result = new List<int>();
            for (int i = 0; i < doubles.Count; i++)
            {
                double d = doubles[i];
                d *= (maxValue - minValue);
                d += minValue;
                if (d < minValue)
                {
                    d = minValue;
                }
                if (d >= maxValue)
                {
                    d = maxValue - 1;
                }
                result.Add((int)d);
            }
            return result;
        }

        /// <summary>
        /// 返回一个大于或等于 0.0 且小于 1.0 的随机浮点数。
        /// <para/>返回结果：大于或等于 0.0 且小于 1.0 的双精度浮点数。
        /// </summary>
        /// <returns></returns>
        private double Simple()
        {
            Buffer.BlockCopy(_hash, 0, _tmp, 0, _tmp.Length);
            if (BitConverter.IsLittleEndian)
            {
                _tmp[6] |= 0xF0;
                _tmp[7] = 0x3F;
            }
            else
            {
                _tmp[0] = 0x3F;
                _tmp[1] |= 0xF0;
            }
            double result = BitConverter.ToDouble(_tmp, 0);
            if (result < 0.1d)
            {
                result *= 10d;
            }
            if (result >= 1d)
            {
                result -= 1d;
            }
            _hash = _hashAlgorithm.ComputeHash(_hash);
            return result;
        }

        #region 随机字符串

        /// <summary>
        /// 返回一个指定字符范围的随机字符串。
        /// <para/>返回结果：指定字符范围的随机字符串。
        /// <para/>字符范围标记：
        /// <para/>'d' 阿拉伯数字，不包括字形易混淆的字符。
        /// <para/>'D' 阿拉伯数字。
        /// <para/>'a' 大写和小写英文字母，不包括字形易混淆的字符。
        /// <para/>'A' 大写和小写英文字母。
        /// <para/>'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。
        /// <para/>'M' 大写和小写英文字母和阿拉伯数字。
        /// <para/>'h' 小写十六进制字符。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string NextString(char token, int count)
        {
            IList<int> positions;
            char[] chars;
            switch (token)
            {
                case 'd': positions = Next(count, 0, _digitalLess.Length); chars = _digitalLess; break;
                case 'D': positions = Next(count, 0, _digital.Length); chars = _digital; break;
                case 'a': positions = Next(count, 0, _alphabetLess.Length); chars = _alphabetLess; break;
                case 'A': positions = Next(count, 0, _alphabet.Length); chars = _alphabet; break;
                case 'm': positions = Next(count, 0, _mixtureLess.Length); chars = _mixtureLess; break;
                case 'M': positions = Next(count, 0, _mixture.Length); chars = _mixture; break;
                case 'h': positions = Next(count, 0, _hex.Length); chars = _hex; break;
                default: positions = null; chars = null; break;
            }
            if (positions is null || chars is null)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder result = new StringBuilder();
                foreach (int position in positions)
                {
                    result.Append(chars[position]);
                }
                return result.ToString();
            }
        }

        /// <summary>
        /// 返回一个指定字符范围的随机字符串。
        /// <para/>返回结果：指定字符范围的随机字符串。
        /// <para/>字符掩码标记：
        /// <para/>'d' 阿拉伯数字，不包括字形易混淆的字符。
        /// <para/>'D' 阿拉伯数字。
        /// <para/>'a' 大写和小写英文字母，不包括字形易混淆的字符。
        /// <para/>'A' 大写和小写英文字母。
        /// <para/>'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。
        /// <para/>'M' 大写和小写英文字母和阿拉伯数字。
        /// <para/>'h' 小写十六进制字符。
        /// <para/>'c' 使用自定义字符。需配合 '@' 指示符同时使用。
        /// <para/>'@' 指示符之后的字符作为自定义字符。需配合 'c' 指示符同时使用。
        /// <para/>'+' 指示符之后的随机字符转换为大写形式。不影响直接输出指示符 '(...)'。
        /// <para/>'-' 指示符之后的随机字符转换为小写形式。不影响直接输出指示符 '(...)'。
        /// <para/>'.' 指示符之后的随机字符不再进行大小写转换。
        /// <para/>'(...)' 指示符之内的字符直接输出，不作为掩码字符。
        /// <para/>'(..!)..)' '!'后一个字符直接输出，主要用于后括号 ')' 输出。
        /// <para/>'[number]' 指示符之内的数字表示输出前一随机字符的个数。
        /// <para/>实例：
        /// <para/>+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm 模拟 Windows 序列号。
        /// <para/>h[8](-)h[4](-)h[4](-)h[4](-)h[12] 模拟 GUID。
        /// <para/>(WPD888-5)DDDD(-)DDDDD(-)DDDDD 模拟 Macromedia 8 序列号。
        /// <para/>(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^* 自定义字符。
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        public string NextString(string mark)
        {
            if (_rooms is null)
            {
                InitRooms();
            }
            else
            {
                foreach (KeyValuePair<char, Room> room in _rooms)
                {
                    room.Value.Reset();
                }
            }
            //
            char[] customs = null;
            int offset = mark.IndexOf('@');
            if (offset > 0)
            {
                customs = mark.ToCharArray(offset + 1, mark.Length - offset - 1);
            }
            else
            {
                offset = mark.Length;
            }
            _rooms['c'].Source = customs;
            char[] marks = mark.ToCharArray(0, offset);
            //
            List<char> tags = new List<char>();
            List<char> sens = new List<char>();
            bool direct = false;
            bool directOne = false;
            char sen = '.';
            char cRepeat = '*';
            bool repeat = false;
            StringBuilder number = new StringBuilder();
            foreach (char c in marks)
            {
                if (direct)
                {
                    if (directOne)
                    {
                        tags.Add('!');
                        sens.Add(c);
                        directOne = false;
                    }
                    else
                    {
                        switch (c)
                        {
                            case '!': directOne = true; break;
                            case ')': direct = false; break;
                            default: tags.Add('!'); sens.Add(c); break;
                        }
                    }
                }
                else if (directOne)
                {
                    tags.Add('!');
                    sens.Add(c);
                    directOne = false;
                }
                else if (repeat)
                {
                    switch (c)
                    {
                        case ']':
                            int count = int.Parse(number.ToString());
                            _rooms[cRepeat].Count += count;
                            for (int i = 0; i < count; i++)
                            {
                                tags.Add(cRepeat);
                                sens.Add(sen);
                            }
                            number.Clear();
                            repeat = false;
                            break;

                        default: number.Append(c); break;
                    }
                }
                else
                {
                    switch (c)
                    {
                        case 'd':
                        case 'D':
                        case 'a':
                        case 'A':
                        case 'm':
                        case 'M':
                        case 'h':
                        case 'c': cRepeat = c; tags.Add(c); sens.Add(sen); _rooms[c].Count += 1; break;
                        case '+': sen = '+'; break;
                        case '-': sen = '-'; break;
                        case '.': sen = '.'; break;
                        case '(': direct = true; break;
                        case '!': directOne = true; break;
                        case '[': repeat = true; break;
                        default: throw new ArgumentException($"The mask string contains invalid character - \"{c}\".");
                    }
                }
            }
            foreach (KeyValuePair<char, Room> room in _rooms)
            {
                room.Value.GenerateRand();
            }
            char[] chars = sens.ToArray();
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i] != '!')
                {
                    Room room = _rooms[tags[i]];
                    chars[i] = room.Rand[room.Index];
                    room.Index++;
                }
            }
            string upp = new string(chars);
            upp = upp.ToUpperInvariant();
            string low = upp.ToLowerInvariant();
            for (int i = 0; i < sens.Count; i++)
            {
                if (sens[i] == '+')
                {
                    chars[i] = upp[i];
                }
                else if (sens[i] == '-')
                {
                    chars[i] = low[i];
                }
            }
            return new string(chars);
        }

        private void InitRooms()
        {
            _rooms = new Dictionary<char, Room>
            {
                { 'd', new Room(this, _digitalLess) },
                { 'D', new Room(this, _digital) },
                { 'a', new Room(this, _alphabetLess) },
                { 'A', new Room(this, _alphabet) },
                { 'm', new Room(this, _mixtureLess) },
                { 'M', new Room(this, _mixture) },
                { 'h', new Room(this, _hex) },
                { 'c', new Room(this) }
            };
        }

        private sealed class Room
        {
            private readonly Randoom _randoom;

            internal Room(Randoom randoom) : this(randoom, null)
            {
            }

            internal Room(Randoom randoom, char[] source)
            {
                _randoom = randoom;
                Source = source;
            }

            internal int Count { get; set; }

            internal int Index { get; set; }

            internal char[] Rand { get; set; }

            internal char[] Source { get; set; }

            internal void GenerateRand()
            {
                if (Count > 0)
                {
                    if (Source is null)
                    {
                        Rand = new char[Count];
                        for (int i = 0; i < Rand.Length; i++)
                        {
                            Rand[i] = 'c';
                        }
                    }
                    else
                    {
                        IList<int> positions = _randoom.Next(Count, 0, Source.Length);
                        List<char> chars = new List<char>();
                        foreach (int position in positions)
                        {
                            chars.Add(Source[position]);
                        }
                        Rand = chars.ToArray();
                    }
                }
            }

            internal void Reset()
            {
                Count = 0;
                Rand = null;
                Index = 0;
            }
        }

        #endregion 随机字符串
    }
}