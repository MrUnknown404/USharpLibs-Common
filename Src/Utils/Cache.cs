using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public class Cache<K, V> where K : notnull {
		public K? Key { get; private set; }
		public V? Value { get; private set; }
		public bool IsSet { get; private set; }

		public V? Set(K key, V? value) {
			Key = key;
			Value = value;
			IsSet = true;
			return value;
		}

		public bool Is(K? key) => IsSet && Key != null && Key.Equals(key);

		public void Clear() {
			Key = default;
			Value = default;
			IsSet = false;
		}
	}
}