namespace USharpLibs.Common.Utils {
	public sealed class SetOnce<T> {
		private T? value;

		public T? Value {
			get => value;
			set {
				if (!hasValue) {
					this.value = value;
					hasValue = true;
				}
			}
		}

		private bool hasValue;
	}
}