using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Honoo
{
    public partial class Randoom
    {
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

        /// <summary>
        /// 返回一个指定字符范围的随机字符串。
        /// <para/>字符范围标记：
        /// <br/><see langword="D"/> 或 <see langword="0"/> 阿拉伯数字。
        /// <br/><see langword="d"/> 或 <see langword="1"/> 阿拉伯数字，不包括字形易混淆的字符。
        /// <br/><see langword="A"/> 大写和小写英文字母。
        /// <br/><see langword="a"/> 大写和小写英文字母，不包括字形易混淆的字符。
        /// <br/><see langword="M"/> 大写和小写英文字母和阿拉伯数字。
        /// <br/><see langword="m"/> 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。
        /// <br/><see langword="h"/> 小写十六进制字符。
        /// </summary>
        /// <param name="charCount">要生成的字符个数。</param>
        /// <param name="token">字符范围标记。</param>
        /// <returns></returns>
        public string NextString(int charCount, char token)
        {
            char[] source;
            List<int> positions;
            switch (token)
            {
                case 'D': case '0': positions = GetPositions(_digital.Length, charCount); source = _digital; break;
                case 'd': case '1': positions = GetPositions(_digitalLess.Length, charCount); source = _digitalLess; break;
                case 'A': positions = GetPositions(_alphabet.Length, charCount); source = _alphabet; break;
                case 'a': positions = GetPositions(_alphabetLess.Length, charCount); source = _alphabetLess; break;
                case 'M': positions = GetPositions(_mixture.Length, charCount); source = _mixture; break;
                case 'm': positions = GetPositions(_mixtureLess.Length, charCount); source = _mixtureLess; break;
                case 'h': positions = GetPositions(_hex.Length, charCount); source = _hex; break;
                default: positions = null; source = null; break;
            }
            if (source is null)
            {
                throw new ArgumentException($"The token invalid.");
            }
            else
            {
                var result = new StringBuilder();
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
        /// <param name="charCount">要生成的字符个数。</param>
        /// <param name="customSource">自定义字符集合。</param>
        /// <returns></returns>
        public string NextString(int charCount, string customSource)
        {
            if (string.IsNullOrEmpty(customSource))
            {
                throw new ArgumentException($"The customSource invalid.");
            }
            char[] source = customSource.ToCharArray();
            List<int> positions = GetPositions(customSource.Length, charCount);
            var result = new StringBuilder();
            foreach (int position in positions)
            {
                result.Append(source[position]);
            }
            return result.ToString();
        }

        /// <summary>
        /// 返回一个由掩码定义的随机字符串。
        /// <para/>字符范围标记和控制符：
        /// <br/><see langword="D"/> 或 <see langword="0"/> 阿拉伯数字。
        /// <br/><see langword="d"/> 或 <see langword="1"/> 阿拉伯数字，不包括字形易混淆的字符。
        /// <br/><see langword="A"/> 大写和小写英文字母。
        /// <br/><see langword="a"/> 大写和小写英文字母，不包括字形易混淆的字符。
        /// <br/><see langword="M"/> 大写和小写英文字母和阿拉伯数字。
        /// <br/><see langword="m"/> 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。
        /// <br/><see langword="h"/> 小写十六进制字符。
        /// <br/><see langword="c"/> 使用自定义字符集合。需配合 <see langword="@"/> 控制符同时使用。
        /// <br/><see langword="["/>number<see langword="]"/> 方括号控制符之内的数字表示输出前一随机字符的个数。
        /// <br/><see langword="@"/> 控制符之后的字符作为自定义字符。需配合 <see langword="c"/> 标记同时使用。
        /// <br/><see langword="{"/>xxx<see langword="}"/> 大括号控制符之内的字符直接输出，不作为掩码字符。
        /// <br/><see langword="!"/> 叹号控制符后一个字符直接输出。在 <see langword="{"/>xx<see langword="!"/>}xx<see langword="}"/> 中可用于输出后括号 '}'。
        /// <br/><see langword="+"/> 加号控制符之后的随机字符转换为大写形式。不影响直接输出控制符 <see langword="{"/>xxx<see langword="}"/>。
        /// <br/><see langword="-"/> 减号控制符之后的随机字符转换为小写形式。不影响直接输出控制符 <see langword="{"/>xxx<see langword="}"/>。
        /// <br/><see langword="."/> 点号控制符之后的随机字符不再进行大小写转换。
        /// <para/>实例：
        /// <br/>+mmmmm{-}mmmmm{-}mmmmm{-}mmmmm{-}mmmmm ||| 模拟 Windows 序列号。
        /// <br/>h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12] ||| 模拟 GUID。
        /// <br/>{WPD888-5}DDDD{-}DDDDD{-}DDDDD ||| 模拟 Macromedia 8 序列号。
        /// <br/>ccccccccccccccccccccccccc@ABCabc12345~!@#$%^* ||| 自定义字符。
        /// </summary>
        /// <param name="mask">由字符范围标记和控制符组成的掩码字符串。</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:将字符串规范化为大写", Justification = "<挂起>")]
        public string NextString(string mask)
        {
            if (string.IsNullOrWhiteSpace(mask))
            {
                throw new ArgumentException($"\"{nameof(mask)}\" cannot be null or blank.", nameof(mask));
            }
            if (_rooms is null)
            {
                _rooms = new Dictionary<char, Room>
                {
                    { 'D', new Room( _digital) },
                    { 'd', new Room( _digitalLess) },
                    { 'A', new Room( _alphabet) },
                    { 'a', new Room( _alphabetLess) },
                    { 'M', new Room( _mixture) },
                    { 'm', new Room( _mixtureLess) },
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
            int offset = mask.IndexOf('@');
            if (offset > 0)
            {
                char[] customs = mask.ToCharArray(offset + 1, mask.Length - offset - 1);
                _rooms['c'].ReplaceSource(customs);
            }
            else
            {
                offset = mask.Length;
            }

            char[] marks = mask.ToCharArray(0, offset);
            //
            var tags = new List<char>();
            var sens = new List<char>();
            bool direct = false;
            bool directOne = false;
            char sen = '.';
            bool senChanged = false;
            char cRepeat = '*';
            bool repeat = false;
            var number = new StringBuilder();
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
                            case '}': direct = false; break;
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
                        case '0': cRepeat = 'D'; tags.Add('D'); sens.Add(sen); _rooms['D'].Increment(1); break;
                        case '1': cRepeat = 'd'; tags.Add('d'); sens.Add(sen); _rooms['d'].Increment(1); break;
                        case 'D':
                        case 'd':
                        case 'A':
                        case 'a':
                        case 'M':
                        case 'm':
                        case 'h':
                        case 'c': cRepeat = c; tags.Add(c); sens.Add(sen); _rooms[c].Increment(1); break;
                        case '+': sen = '+'; senChanged = true; break;
                        case '-': sen = '-'; senChanged = true; break;
                        case '.': sen = '.'; break;
                        case '{': direct = true; break;
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
                var upp = new string(chars);
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

        private List<int> GetPositions(int maxValue, int count)
        {
            var result = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                result.Add(Next(maxValue));
            }
            return result;
        }
    }
}