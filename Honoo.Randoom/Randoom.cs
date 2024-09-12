using System;
using System.Collections.Generic;
using System.Globalization;
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
        private int _hashIndex;
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
            if (_hashAlgorithm.HashSize < 64)
            {
                throw new CryptographicException("Hash algorithm's hash size must be more than 64 bits.");
            }
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
        /// 释放由 <see cref="Randoom"/> 使用的资源。
        /// </summary>
        /// <param name="disposing">释放非托管资源。</param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _rooms = null;
                    _hash = null;
                    _tmp = null;
                }
                _hashAlgorithm.Dispose();
                _hashAlgorithm = null;
                _disposed = true;
            }
        }

        #endregion 构造

        /// <summary>
        /// 返回一个非负随机整数。
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return Next(0, int.MaxValue);
        }

        /// <summary>
        /// 返回一个小于所指定最大值的非负随机整数。
        /// </summary>
        /// <param name="maxValue">返回的随机数的上界（随机数不可取该上界值）。</param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        /// <summary>
        /// 返回在指定范围内的任意整数。
        /// </summary>
        /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
        /// <param name="maxValue">返回的随机数的上界（随机数不可取该上界值）。</param>
        /// <returns></returns>
        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }
            double value = NextDouble();
            value *= (maxValue - minValue);
            value += minValue;
            return (int)value;
        }

        /// <summary>
        /// 用加强型随机值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        public void NextBytes(byte[] buffer)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            NextBytes(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 用加强型随机值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        /// <param name="offset">从 <paramref name="buffer"/> 的指定偏移处开始填充。</param>
        /// <param name="length">要填充的长度。</param>
        public void NextBytes(byte[] buffer, int offset, int length)
        {
            while (length > 0)
            {
                int len = Math.Min(_hash.Length - _hashIndex, length);
                Buffer.BlockCopy(_hash, _hashIndex, buffer, offset, len);
                length -= len;
                offset += len;
                _hashIndex += len;
                if (_hashIndex >= _hash.Length)
                {
                    _hash = _hashAlgorithm.ComputeHash(_hash);
                    _hashIndex = 0;
                }
            }
        }

        /// <summary>
        /// 返回一个大于或等于 0.0 且小于 1.0 的随机浮点数。
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            if (_hash.Length - _hashIndex < 8)
            {
                _hash = _hashAlgorithm.ComputeHash(_hash);
                _hashIndex = 0;
            }
            Buffer.BlockCopy(_hash, _hashIndex, _tmp, 0, 8);
            _hashIndex += 8;
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
                result -= (int)result;
            }
            if (_hashIndex >= _hash.Length)
            {
                _hash = _hashAlgorithm.ComputeHash(_hash);
                _hashIndex = 0;
            }
            return result;
        }

        /// <summary>
        /// 用加强型随机非零值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        public void NextNonZeroBytes(byte[] buffer)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            NextNonZeroBytes(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 用加强型随机非零值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        /// <param name="offset">从 <paramref name="buffer"/> 的指定偏移处开始填充。</param>
        /// <param name="length">要填充的长度。</param>
        public void NextNonZeroBytes(byte[] buffer, int offset, int length)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            while (length > 0)
            {
                while (length > 0 && _hashIndex < _hash.Length)
                {
                    if (_hash[_hashIndex] != 0)
                    {
                        buffer[offset] = _hash[_hashIndex];
                        offset++;
                        length--;
                    }
                    _hashIndex++;
                }
                if (_hashIndex >= _hash.Length)
                {
                    _hash = _hashAlgorithm.ComputeHash(_hash);
                    _hashIndex = 0;
                }
            }
        }

        #region 随机字符串

        /// <summary>
        /// 返回一个指定字符范围的随机字符串。
        /// <para/>字符范围标记：
        /// <br/>'d' 阿拉伯数字，不包括字形易混淆的字符。
        /// <br/>'D' 阿拉伯数字。
        /// <br/>'1' 阿拉伯数字，不包括字形易混淆的字符。'd' 的别名。
        /// <br/>'0' 阿拉伯数字。'D' 的别名。
        /// <br/>'a' 大写和小写英文字母，不包括字形易混淆的字符。
        /// <br/>'A' 大写和小写英文字母。
        /// <br/>'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。
        /// <br/>'M' 大写和小写英文字母和阿拉伯数字。
        /// <br/>'h' 小写十六进制字符。
        /// </summary>
        /// <param name="count">要生成的字符个数。</param>
        /// <param name="token">字符范围标记。</param>
        /// <returns></returns>
        public string NextString(int count, char token)
        {
            char[] source;
            IList<int> positions;
            switch (token)
            {
                case 'd': case '1': positions = Next(count, 0, _digitalLess.Length); source = _digitalLess; break;
                case 'D': case '0': positions = Next(count, 0, _digital.Length); source = _digital; break;
                case 'a': positions = Next(count, 0, _alphabetLess.Length); source = _alphabetLess; break;
                case 'A': positions = Next(count, 0, _alphabet.Length); source = _alphabet; break;
                case 'm': positions = Next(count, 0, _mixtureLess.Length); source = _mixtureLess; break;
                case 'M': positions = Next(count, 0, _mixture.Length); source = _mixture; break;
                case 'h': positions = Next(count, 0, _hex.Length); source = _hex; break;
                default: positions = null; source = null; break;
            }
            if (source is null)
            {
                throw new ArgumentException($"The token invalid.");
            }
            else
            {
                StringBuilder result = new StringBuilder();
                foreach (int position in positions)
                {
                    result.Append(source[position]);
                }
                return result.ToString();
            }
        }

        /// <summary>
        /// 返回一个由自定义字符组成的随机字符串。
        /// </summary>
        /// <param name="count">要生成的字符个数。</param>
        /// <param name="customSource">自定义字符集合。</param>
        /// <returns></returns>
        public string NextString(int count, string customSource)
        {
            if (string.IsNullOrEmpty(customSource))
            {
                throw new ArgumentException($"The customSource invalid.");
            }
            char[] source = customSource.ToCharArray();
            IList<int> positions = Next(count, 0, customSource.Length);
            StringBuilder result = new StringBuilder();
            foreach (int position in positions)
            {
                result.Append(source[position]);
            }
            return result.ToString();
        }

        /// <summary>
        /// 返回一个由掩码定义的随机字符串。
        /// <para/>字符范围标记和控制符：
        /// <br/>'d' 阿拉伯数字，不包括字形易混淆的字符。
        /// <br/>'D' 阿拉伯数字。
        /// <br/>'1' 阿拉伯数字，不包括字形易混淆的字符。'd' 的别名。
        /// <br/>'0' 阿拉伯数字。'D' 的别名。
        /// <br/>'a' 大写和小写英文字母，不包括字形易混淆的字符。
        /// <br/>'A' 大写和小写英文字母。
        /// <br/>'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。
        /// <br/>'M' 大写和小写英文字母和阿拉伯数字。
        /// <br/>'h' 小写十六进制字符。
        /// <br/>'c' 使用自定义字符集合。需配合 '@' 控制符同时使用。
        /// <br/>'@' 控制符之后的字符作为自定义字符。需配合 'c' 标记同时使用。
        /// <br/>'+' 控制符之后的随机字符转换为大写形式。不影响直接输出控制符 '(...)'。
        /// <br/>'-' 控制符之后的随机字符转换为小写形式。不影响直接输出控制符 '(...)'。
        /// <br/>'.' 控制符之后的随机字符不再进行大小写转换。
        /// <br/>'(...)' 控制符之内的字符直接输出，不作为掩码字符。
        /// <br/>'(..!)..)' '!'后一个字符直接输出，主要用于后括号 ')' 输出。
        /// <br/>'[number]' 控制符之内的数字表示输出前一随机字符的个数。
        /// <para/>实例：
        /// <br/>+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm 模拟 Windows 序列号。
        /// <br/>h[8](-)h[4](-)h[4](-)h[4](-)h[12] 模拟 GUID。
        /// <br/>(WPD888-5)DDDD(-)DDDDD(-)DDDDD 模拟 Macromedia 8 序列号。
        /// <br/>ccccccccccccccccccccccccc@ABCabc12345~!@#$%^* 自定义字符。
        /// </summary>
        /// <param name="mark">由字符范围标记和控制符组成的掩码字符串。</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:将字符串规范化为大写", Justification = "<挂起>")]
        public string NextString(string mark)
        {
            if (string.IsNullOrWhiteSpace(mark))
            {
                throw new ArgumentException($"\"{nameof(mark)}\" cannot be null or blank.", nameof(mark));
            }
            if (_rooms is null)
            {
                _rooms = new Dictionary<char, Room>
                {
                    { 'd', new Room( _digitalLess) },
                    { 'D', new Room( _digital) },
                    { 'a', new Room( _alphabetLess) },
                    { 'A', new Room( _alphabet) },
                    { 'm', new Room( _mixtureLess) },
                    { 'M', new Room( _mixture) },
                    { 'h', new Room( _hex) },
                    { 'c', new Room() }
                };
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
            _rooms['c'].ReplaceSource(customs);
            char[] marks = mark.ToCharArray(0, offset);
            //
            List<char> tags = new List<char>();
            List<char> sens = new List<char>();
            bool direct = false;
            bool directOne = false;
            char sen = '.';
            bool senChanged = false;
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
                            int count = int.Parse(number.ToString(), CultureInfo.InvariantCulture);
                            count -= 1;
                            _rooms[cRepeat].Increment(count);
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
                        case '1': cRepeat = 'd'; tags.Add('d'); sens.Add(sen); _rooms['d'].Increment(1); break;
                        case '0': cRepeat = 'D'; tags.Add('D'); sens.Add(sen); _rooms['D'].Increment(1); break;
                        case 'd':
                        case 'D':
                        case 'a':
                        case 'A':
                        case 'm':
                        case 'M':
                        case 'h':
                        case 'c': cRepeat = c; tags.Add(c); sens.Add(sen); _rooms[c].Increment(1); break;
                        case '+': sen = '+'; senChanged = true; break;
                        case '-': sen = '-'; senChanged = true; break;
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
                room.Value.GenerateRand(this);
            }
            char[] chars = sens.ToArray();
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i] != '!')
                {
                    Room room = _rooms[tags[i]];
                    chars[i] = room.Pick();
                }
            }
            if (senChanged)
            {
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
            }
            return new string(chars);
        }

        private List<int> Next(int count, int minValue, int maxValue)
        {
            List<int> result = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                result.Add(Next(minValue, maxValue));
            }
            return result;
        }

        private sealed class Room
        {
            private int _count;
            private int _index;
            private char[] _rand;
            private char[] _source;

            internal Room() : this(null)
            {
            }

            internal Room(char[] source)
            {
                _source = source;
            }

            internal void GenerateRand(Randoom randoom)
            {
                if (_count > 0)
                {
                    if (_source is null)
                    {
                        _rand = new char[_count];
                        for (int i = 0; i < _rand.Length; i++)
                        {
                            _rand[i] = 'c';
                        }
                    }
                    else
                    {
                        IList<int> positions = randoom.Next(_count, 0, _source.Length);
                        List<char> chars = new List<char>();
                        foreach (int position in positions)
                        {
                            chars.Add(_source[position]);
                        }
                        _rand = chars.ToArray();
                    }
                }
            }

            internal void Increment(int count)
            {
                _count += count;
            }

            internal char Pick()
            {
                char result = _rand[_index];
                _index++;
                return result;
            }

            internal void ReplaceSource(char[] source)
            {
                _source = source;
            }

            internal void Reset()
            {
                _count = 0;
                _rand = null;
                _index = 0;
            }
        }

        #endregion 随机字符串
    }
}