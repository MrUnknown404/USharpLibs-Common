using System.Collections;

namespace USharpLibs.Common.Collections {
	// This could probably be more efficient?
	[PublicAPI]
	public class LookupDictionary<K, L, V> : IEnumerable<(L, K, V)> where K : notnull where L : notnull {
		private Dictionary<L, K> LookupDict { get; } = new();
		private Dictionary<K, V> ValueDict { get; } = new();

		private IEnumerable<L> Lookups => LookupDict.Keys;
		private IEnumerable<K> Keys => ValueDict.Keys;
		private IEnumerable<V> Values => ValueDict.Values;

		public int Count => ValueDict.Count;

		public V this[L lookup] => ValueDict[LookupDict[lookup]];
		public V this[K key] => ValueDict[key];

		public V this[K key, L lookup] {
			set {
				LookupDict[lookup] = key;
				ValueDict[key] = value;
			}
		}

		public bool Contains(L lookup) => LookupDict.ContainsKey(lookup);
		public bool Contains(K key) => ValueDict.ContainsKey(key);
		public bool ContainsValue(V value) => ValueDict.ContainsValue(value);

		public IEnumerator<(L, K, V)> GetEnumerator() {
			foreach ((L lookup, K key) in LookupDict) { yield return new(lookup, key, ValueDict[key]); }
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}