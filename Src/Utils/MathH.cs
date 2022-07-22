namespace USharpLibs.Common.Utils {
	public static class MathH {
		public static double ToRadians(double degrees) => degrees * Math.PI / 180d;
		public static double ToDegrees(double rads) => rads * 180d / Math.PI;
	}
}