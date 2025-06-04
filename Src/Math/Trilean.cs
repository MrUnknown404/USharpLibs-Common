using JetBrains.Annotations;

namespace USharpLibs.Common.Math {
	// TODO redo this way nicer
	public enum Trilean : byte {
		Unknown = 0,
		False = 1,
		True = 2,
	}

	[PublicAPI]
	public static class TrileanExtensions {
		public static bool IsUnknown(this Trilean self) => self == Trilean.Unknown;
		public static bool IsFalse(this Trilean self) => self == Trilean.False;
		public static bool IsTrue(this Trilean self) => self == Trilean.True;
		public static Trilean BoolToTrilean(bool b) => b ? Trilean.True : Trilean.False;
	}
}