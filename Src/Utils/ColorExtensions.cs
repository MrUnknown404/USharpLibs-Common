using System.Drawing;
using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class ColorExtensions {
		extension(Color self) {
			[MustUseReturnValue] public string ToHex(bool includeHash = true) => $"{(includeHash ? '#' : string.Empty)}{self.R:X2}{self.G:X2}{self.B:X2}";
		}
	}
}