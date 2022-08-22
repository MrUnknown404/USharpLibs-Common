namespace USharpLibs.Common.Utils {
	public static class MathH {
		public static double ToRadians(double degrees) => degrees * Math.PI / 180d;
		public static double ToDegrees(double rads) => rads * 180d / Math.PI;

		public static int Floor(float value) => (int)MathF.Floor(value);
		public static long Floor(double value) => (long)Math.Floor(value);
		public static int Floor(decimal value) => (int)Math.Floor(value);

		public static int Ceil(float value) => (int)MathF.Ceiling(value);
		public static long Ceil(double value) => (long)Math.Ceiling(value);
		public static int Ceil(decimal value) => (int)Math.Ceiling(value);

		public static int Round(float value) => (int)MathF.Round(value);
		public static long Round(double value) => (long)Math.Round(value);
		public static int Round(decimal value) => (int)Math.Round(value);
		public static int Round(float value, MidpointRounding rounding) => (int)MathF.Round(value, rounding);
		public static long Round(double value, MidpointRounding rounding) => (long)Math.Round(value, rounding);
		public static int Round(decimal value, MidpointRounding rounding) => (int)Math.Round(value, rounding);
	}
}