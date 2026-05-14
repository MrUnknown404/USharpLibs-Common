using System.Numerics;
using JetBrains.Annotations;

namespace USharpLibs.Common.Utils;

[PublicAPI]
public static class VectorExtensions {
	extension(Vector2 self) {
		public bool IsAnyNaN => float.IsNaN(self.X) || float.IsNaN(self.Y);
	}
}