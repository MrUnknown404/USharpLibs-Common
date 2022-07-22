namespace USharpLibs.Common.Utils.Extensions {
	public static class CollectionExtension {
		public static string AsString<T>(this T[] self) => $"Size:{self.Length}, Elements:[{string.Join(", ", self)}]";
		public static string AsString<T>(this ICollection<T> self) => $"Size:{self.Count}, Elements:[{string.Join(", ", self)}]";
	}

	public static class DictionaryExtension {
		public static V ComputeIfAbsent<K, V>(this Dictionary<K, V> self, K key, Func<K, V> compute) where K : notnull {
			if (self.ContainsKey(key)) { return self[key]; }

			V value = compute(key);
			self.Add(key, value);
			return value;
		}

		public static V? ComputeIfPresent<K, V>(this Dictionary<K, V> self, K key, Func<K, V, V> compute) where K : notnull => self.ContainsKey(key) ? self[key] = compute(key, self[key]) : default;

		public static V? ComputeForBoth<K, V>(this Dictionary<K, V> self, K key, Func<K, V, V> ifPresent, Func<K, V> ifMissing) where K : notnull => self.ContainsKey(key) ? self.ComputeIfPresent(key, ifPresent) : self.ComputeIfAbsent(key, ifMissing);

		public static void RunForBoth<K, V>(this Dictionary<K, V> self, K key, Action<K, V> runIfPresent, Action<K> runIfMissing) where K : notnull {
			if (self.ContainsKey(key)) {
				runIfPresent(key, self[key]);
			} else {
				runIfMissing(key);
			}
		}

		public static V? TryGetValue<K, V>(this Dictionary<K, V> self, K key) where K : notnull {
			self.TryGetValue(key, out V? value);
			return value;
		}
	}

	public static class EnumerableExtension {
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
			if (action == null) { throw new ArgumentNullException(nameof(action)); }
			foreach (T item in source) { action(item); }
		}
	}
}