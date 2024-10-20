/*
 * 适用 net40;net462;netstandard2.0;net6.0;net8.0;
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Honoo
{
    /// <summary>
    /// 随机值生成器。
    /// </summary>
    public partial class Randoom : IDisposable
    {
        #region 成员

        private bool _disposed;
        private byte[] _hash;
        private HashAlgorithm _hashAlgorithm;
        private int _hashIndex;
        private Dictionary<char, Room> _rooms;

        #endregion 成员

        #region 构造

        /// <summary>
        /// 创建 Randoom 的新实例。
        /// </summary>
        public Randoom() : this(null, HashAlgorithm.Create("SHA256"))
        {
        }

        /// <summary>
        /// 创建 Randoom 的新实例。
        /// </summary>
        /// <param name="seed">额外的种子，通常采集终端用户的鼠标、按键等行为生成。</param>
        public Randoom(byte[] seed) : this(seed, HashAlgorithm.Create("SHA256"))
        {
        }

        /// <summary>
        /// 创建 Randoom 的新实例。
        /// </summary>
        /// <param name="seed">额外的种子，通常采集终端用户的鼠标、按键等行为生成。</param>
        /// <param name="hashAlgorithm">用于随机数生成的 hash 算法实例。必须是大于等于 64 bits 的算法。此 hash 算法实例跟随 Randoom 实例释放。</param>
        public Randoom(byte[] seed, HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException(nameof(hashAlgorithm));
            if (_hashAlgorithm.HashSize < 64)
            {
                throw new CryptographicException("Hash algorithm's hash size must be greater than or equal to 64 bits.");
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
        /// <param name="disposing"><see langword="true"/> 释放托管资源和非托管资源。<see langword="false"/> 仅释放非托管资源。</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _rooms = null;
                    _hash = null;
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
            return SampleInt32();
        }

        /// <summary>
        /// 返回一个小于所指定最大值的非负随机整数。
        /// </summary>
        /// <param name="maxValue">返回的随机数的上界（随机数不可取该上界值）。</param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }
            else if (maxValue > 0)
            {
                return (int)(SampleDouble() * maxValue);
            }
            else
            {
                return maxValue;
            }
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
            else if (minValue < maxValue)
            {
                double value = SampleDouble();
                value *= maxValue - minValue;
                value += minValue;
                return (int)value;
            }
            else
            {
                return minValue;
            }
        }

        /// <summary>
        /// 用随机值序列填充字节数组。
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
        /// 用随机值序列填充字节数组。
        /// </summary>
        /// <param name="buffer">要填充的字节数组。</param>
        /// <param name="offset">从 <paramref name="buffer"/> 的指定偏移处开始填充。</param>
        /// <param name="length">要填充的长度。</param>
        public void NextBytes(byte[] buffer, int offset, int length)
        {
            if (buffer is null)
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
                Buffer.BlockCopy(_hash, _hashIndex, buffer, offset, len);
                length -= len;
                offset += len;
                _hashIndex += len;
            }
        }

        /// <summary>
        /// 返回一个大于或等于 0.0 且小于 1.0 的随机浮点数。
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            return SampleDouble();
        }

        /// <summary>
        /// 返回一个非负随机整数。
        /// </summary>
        /// <returns></returns>
        public long NextInt64()
        {
            return SampleInt64();
        }

        /// <summary>
        /// 返回一个小于所指定最大值的非负随机整数。
        /// </summary>
        /// <param name="maxValue">返回的随机数的上界（随机数不可取该上界值）。</param>
        /// <returns></returns>
        public long NextInt64(long maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }
            else if (maxValue > 0)
            {
                return (long)(SampleDouble() * maxValue);
            }
            else
            {
                return maxValue;
            }
        }

        /// <summary>
        /// 返回在指定范围内的任意整数。
        /// </summary>
        /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
        /// <param name="maxValue">返回的随机数的上界（随机数不可取该上界值）。</param>
        /// <returns></returns>
        public long NextInt64(long minValue, long maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }
            else if (minValue < maxValue)
            {
                double tmp = SampleDouble();
                tmp *= maxValue - minValue;
                tmp += minValue;
                long value1 = (long)tmp;
                value1 <<= 32;
                value1 >>= 32;
                tmp = SampleDouble();
                maxValue >>= 32;
                minValue >>= 32;
                tmp *= maxValue - minValue;
                tmp += minValue;
                long value2 = (long)tmp;
                value2 <<= 32;
                return value2 ^ value1;
            }
            else
            {
                return minValue;
            }
        }

        /// <summary>
        /// 用随机非零值序列填充字节数组。
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
        /// 用随机非零值序列填充字节数组。
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

        /// <summary>
        /// 返回一个大于或等于 0.0 且小于 1.0 的随机浮点数。
        /// </summary>
        /// <returns></returns>
        public float NextSingle()
        {
            return SampleInt32() * (1f / int.MaxValue);
        }

        private double SampleDouble()
        {
            return SampleInt32() * (1d / int.MaxValue);
        }

        private int SampleInt32()
        {
            if (_hash.Length - _hashIndex < 4)
            {
                _hash = _hashAlgorithm.ComputeHash(_hash);
                _hashIndex = 0;
            }
            int result = BitConverter.ToInt32(_hash, _hashIndex);
            _hashIndex += 4;
            if (result == int.MaxValue) result--;
            if (result < 0) result += int.MaxValue;
            return result;
        }

        private long SampleInt64()
        {
            if (_hash.Length - _hashIndex < 8)
            {
                _hash = _hashAlgorithm.ComputeHash(_hash);
                _hashIndex = 0;
            }
            long result = BitConverter.ToInt64(_hash, _hashIndex);
            _hashIndex += 8;
            if (result == long.MaxValue) result--;
            if (result < 0) result += long.MaxValue;
            return result;
        }
    }
}