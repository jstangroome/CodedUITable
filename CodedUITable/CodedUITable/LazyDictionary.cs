using System;
using System.Collections.Concurrent;

namespace CodedUITable
{
    public class LazyDictionary<TKey, TValue>
    {
        private readonly Func<TKey, TValue> _valueFactory;
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary;

        public LazyDictionary(Func<TKey, TValue> valueFactory)
        {
            _valueFactory = valueFactory;
            _dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        public TValue Get(TKey key)
        {
            return _dictionary.GetOrAdd(key, _valueFactory);
        }

        public void Set(TKey key, TValue value)
        {
            _dictionary[key] = value;
        }
    }
}