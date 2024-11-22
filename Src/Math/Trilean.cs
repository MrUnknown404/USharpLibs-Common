namespace USharpLibs.Common.Math {
	// TODO redo this way nicer
	public enum Trilean : byte {
		Unknown = 0,
		True = 2,
		False = 1,
	}

	[PublicAPI]
	public static class TrileanExtensions {
		public static bool IsUnknown(this Trilean self) => self == Trilean.Unknown;
		public static bool IsTrue(this Trilean self) => self == Trilean.True;
		public static bool IsFalse(this Trilean self) => self == Trilean.False;
		public static Trilean BoolToTrilean(bool b) => b ? Trilean.True : Trilean.False;
	}
}