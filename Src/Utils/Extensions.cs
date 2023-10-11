using System.Drawing;
using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class CollectionExtension {
		[MustUseReturnValue] public static string AsString<T>(this T[] self) => $"Size:{self.Length}, Elements:[{string.Join(", ", self)}]";
		[MustUseReturnValue] public static string AsString<T>(this ICollection<T> self) => $"Size:{self.Count}, Elements:[{string.Join(", ", self)}]";
	}

	[PublicAPI]
	public static class DictionaryExtension {
		[MustUseReturnValue]
		public static V ComputeIfAbsent<K, V>(this Dictionary<K, V> self, K key, Func<K, V> compute) where K : notnull {
			if (self.TryGetValue(key, out V? absent)) { return absent; }

			V value = compute(key);
			self.Add(key, value);
			return value;
		}

		[MustUseReturnValue]
		public static V? ComputeIfPresent<K, V>(this Dictionary<K, V> self, K key, Func<K, V, V> compute) where K : notnull =>
				self.ContainsKey(key) ? self[key] = compute(key, self[key]) : default;

		[MustUseReturnValue]
		public static V? ComputeForBoth<K, V>(this Dictionary<K, V> self, K key, Func<K, V, V> ifPresent, Func<K, V> ifMissing) where K : notnull =>
				self.ContainsKey(key) ? self.ComputeIfPresent(key, ifPresent) : self.ComputeIfAbsent(key, ifMissing);

		[MustUseReturnValue]
		public static V? TryGetValue<K, V>(this Dictionary<K, V> self, K key) where K : notnull {
			self.TryGetValue(key, out V? value);
			return value;
		}
	}

	[PublicAPI]
	public static class EnumerableExtension {
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
			if (action == null) { throw new ArgumentNullException(nameof(action)); }

			foreach (T item in source) { action(item); }
		}
	}

	[PublicAPI]
	public static class ColorExtension {
		public static string ToHex(this Color self) => $"#{self.R:X2}{self.G:X2}{self.B:X2}";
	}
}