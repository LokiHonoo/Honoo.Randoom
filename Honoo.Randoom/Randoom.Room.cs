using System.Collections.Generic;

namespace Honoo
{
    public partial class Randoom
    {
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
                        var positions = new List<int>(_count);
                        for (int i = 0; i < _count; i++)
                        {
                            positions.Add(randoom.Next(_source.Length));
                        }
                        var chars = new List<char>(_count);
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
    }
}