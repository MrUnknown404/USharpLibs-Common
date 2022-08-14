namespace USharpLibs.Common.Utils {
	public enum Trilean {
		Unknown = 0,
		True = 2,
		False = 1,
	}

	public static class TrileanExtensions {
		public static bool IsUnknown(this Trilean self) => self == Trilean.Unknown;
		public static bool IsTrue(this Trilean self) => self == Trilean.True;
		public static bool IsFalse(this Trilean self) => self == Trilean.False;

		public static Trilean BoolToTrilean(bool b) => b ? Trilean.True : Trilean.False;
	}
}