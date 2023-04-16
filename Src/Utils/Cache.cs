using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class Cache {
		public static Cache<K, V> Create<K, V>(Func<K?> key, Func<V?> value) => new(key, value);
		public static Cache<K, V> Create<K, V>(Func<K?> key, V? value) => new(key, () => value);
		public static Cache<K, V> Create<K, V>(K? key, Func<V?> value) => new(() => key, value);
		public static Cache<K, V> Create<K, V>(K? key, V? value) => new(() => key, () => value);

		public static Cache<K, V> Create<K, V>(Func<K?> key) => new(key, () => default);
		public static Cache<K, V> Create<K, V>(K? key) => new(() => key, () => default);

		public static Cache<K, V> Create<K, V>(Func<V?> value) => new(() => default, value);
		public static Cache<K, V> Create<K, V>(V? value) => new(() => default, () => value);

		public static Cache<K, V> Create<K, V>() => new(() => default, () => default);
	}

	[PublicAPI]
	public class Cache<K, V> {
		private Func<K?> key;
		private Func<V?> value;

		internal Cache(Func<K?> key, Func<V?> value) {
			this.key = key;
			this.value = value;
		}

		public void Set(Func<K> key, Func<V?> value) {
			this.key = key;
			this.value = value;
		}

		public void Set(Func<K> key, V? value) => Set(key, () => value);
		public void Set(K key, Func<V?> value) => Set(() => key, value);
		public void Set(K key, V? value) => Set(() => key, () => value);

		public static bool operator ==(Cache<K, V> a, K b) {
			K? aKey = a.key();
			return (aKey == null && b == null) || (aKey != null && aKey.Equals(b));
		}

		public static bool operator !=(Cache<K, V> a, K b) {
			K? aKey = a.key();
			return (aKey == null && b != null) || (aKey != null && !aKey.Equals(b));
		}

		public bool IsEmpty() => key() == null;
		public V? Get() => value();

		public override bool Equals(object? obj) => obj is Cache<K, V> cache && (cache.key(), cache.value()).Equals((key(), value()));
		public override int GetHashCode() => (key(), value()).GetHashCode();
		public override string ToString() => $"({key}:{value})";
	}
}