using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public class LookupDictionary<K, L, V> where K : notnull where L : notnull {
		private Dictionary<K, V> ValueDict { get; } = new();
		private Dictionary<L, K> LookupDict { get; } = new();

		public V this[K key] => ValueDict[key];
		public V this[L lookup] => ValueDict[LookupDict[lookup]];

		public V this[K key, L lookup] {
			set {
				ValueDict[key] = value;
				LookupDict[lookup] = key;
			}
		}

		public int Count => ValueDict.Count;

		public bool ContainsKey(K key) => ValueDict.ContainsKey(key);
		public bool ContainsLookup(L lookup) => LookupDict.ContainsKey(lookup);
		public bool ContainsKeyOrLookup(K key, L lookup) => ContainsKey(key) || ContainsLookup(lookup);
		public bool ContainsValue(V value) => ValueDict.ContainsValue(value);
	}
}