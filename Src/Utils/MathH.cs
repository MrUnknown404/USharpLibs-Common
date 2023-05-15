using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class MathH {
		public const float Sqrt3 = 1.73205080757f; // MathF.Sqrt(3)
		public const float HalfSqrt3 = Sqrt3 / 2f;

		[Pure] public static double ToRadians(double degrees) => degrees * Math.PI / 180d;
		[Pure] public static double ToDegrees(double rads) => rads * 180d / Math.PI;
		[Pure] public static float ToRadians(float degrees) => degrees * MathF.PI / 180f;
		[Pure] public static float ToDegrees(float rads) => rads * 180f / MathF.PI;

		[Pure] public static int Floor(float value) => (int)MathF.Floor(value);
		[Pure] public static long Floor(double value) => (long)Math.Floor(value);
		[Pure] public static int Floor(decimal value) => (int)Math.Floor(value);

		[Pure] public static int Ceil(float value) => (int)MathF.Ceiling(value);
		[Pure] public static long Ceil(double value) => (long)Math.Ceiling(value);
		[Pure] public static int Ceil(decimal value) => (int)Math.Ceiling(value);

		[Pure] public static int Round(float value) => (int)MathF.Round(value);
		[Pure] public static long Round(double value) => (long)Math.Round(value);
		[Pure] public static int Round(decimal value) => (int)Math.Round(value);
		[Pure] public static int Round(float value, MidpointRounding rounding) => (int)MathF.Round(value, rounding);
		[Pure] public static long Round(double value, MidpointRounding rounding) => (long)Math.Round(value, rounding);
		[Pure] public static int Round(decimal value, MidpointRounding rounding) => (int)Math.Round(value, rounding);
	}
}