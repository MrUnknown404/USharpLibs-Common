using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class CollectionExtensions {
		extension<T>(ICollection<T> self) {
			[MustUseReturnValue] public string GetSizeAndElementsAsString() => $"Size:{self.Count}, Elements:[{string.Join(", ", self)}]";
			[MustUseReturnValue] public string ElementsAsString() => self.Count == 0 ? "[ ]" : $"[ {string.Join(", ", self)} ]";
		}
	}
}