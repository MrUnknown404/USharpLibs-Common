using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace USharpLibs.Common.Utils;

[PublicAPI]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class IEnumerableExtensions {
	extension<T>(IEnumerable<T> self) {
		[MustUseReturnValue]
		public string ElementsAsString() => $"[ {string.Join(", ", self)} ]";
	}
}