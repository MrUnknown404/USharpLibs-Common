namespace USharpLibs.Common.Utils {
	public static class MathH {
		public static double ToRadians(double degrees) => degrees * Math.PI / 180d;
		public static double ToDegrees(double rads) => rads * 180d / Math.PI;
		public static int Floor(float value) => (int)MathF.Floor(value);
		public static int Floor(double value) => (int)Math.Floor(value);
		public static int Floor(decimal value) => (int)Math.Floor(value);
		public static int Ceil(float value) => (int)MathF.Ceiling(value);
		public static int Ceil(double value) => (int)Math.Ceiling(value);
		public static int Ceil(decimal value) => (int)Math.Ceiling(value);
	}
}