#if NET8_0_OR_GREATER

using System;

namespace Honoo
{
    public partial class Randoom
    {
        /// <summary>
        /// 用随机值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        public void NextBytes(Span<byte> buffer)
        {
            if (buffer.IsEmpty)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            NextBytes(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 用随机值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        /// <param name="offset">从 <paramref name="buffer"/> 的指定偏移处开始填充。</param>
        /// <param name="length">要填充的长度。</param>
        public void NextBytes(Span<byte> buffer, int offset, int length)
        {
            if (buffer.IsEmpty)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            while (length > 0)
            {
                if (_hashIndex >= _hash.Length)
                {
                    _hash = _hashAlgorithm.ComputeHash(_hash);
                    _hashIndex = 0;
                }
                int len = Math.Min(_hash.Length - _hashIndex, length);
                for (int i = offset; i < len + offset; i++)
                {
                    buffer[i] = _hash[_hashIndex + i];
                }
                length -= len;
                offset += len;
                _hashIndex += len;
            }
        }

        /// <summary>
        /// 用随机非零值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        public void NextNonZeroBytes(Span<byte> buffer)
        {
            if (buffer.IsEmpty)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            NextNonZeroBytes(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 用随机非零值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        /// <param name="offset">从 <paramref name="buffer"/> 的指定偏移处开始填充。</param>
        /// <param name="length">要填充的长度。</param>
        public void NextNonZeroBytes(Span<byte> buffer, int offset, int length)
        {
            if (buffer.IsEmpty)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            while (length > 0)
            {
                if (_hashIndex >= _hash.Length)
                {
                    _hash = _hashAlgorithm.ComputeHash(_hash);
                    _hashIndex = 0;
                }
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
            }
        }
    }
}

#endif