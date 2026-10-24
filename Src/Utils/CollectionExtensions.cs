using JetBrains.Annotations;

namespace USharpLibs.Common.Utils;

[PublicAPI]
public static class CollectionExtensions {
	extension<T>(ICollection<T> self) {
		[MustUseReturnValue]
		public string GetSizeAndElementsAsString(Func<T, string>? toStringFunc = null) =>
				self.Count == 0 ? "Size: 0, Elements: [ ]" :
				toStringFunc == null ? $"Size: {self.Count}, Elements: [{string.Join(", ", self)}]" : $"Size: {self.Count}, Elements: [{string.Join(", ", self.Select(toStringFunc))}]";

		[MustUseReturnValue]
		public string ElementsAsString(Func<T, string>? toStringFunc = null) =>
				self.Count == 0 ? "[ ]" : toStringFunc == null ? $"[ {string.Join(", ", self)} ]" : $"[ {string.Join<string>(", ", self.Select(toStringFunc))} ]";
	}
}