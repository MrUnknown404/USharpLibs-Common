using System.Drawing;
using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class Extensions {
		extension<T>(ICollection<T> self) {
			[MustUseReturnValue] public string GetSizeAndElementsAsString() => $"Size:{self.Count}, Elements:[{string.Join(", ", self)}]";
		}

		extension(Color self) {
			[MustUseReturnValue] public string ToHex(bool includeHash = true) => $"{(includeHash ? '#' : string.Empty)}{self.R:X2}{self.G:X2}{self.B:X2}";
		}
	}
}