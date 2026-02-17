using JetBrains.Annotations;

namespace USharpLibs.Common.Math {
	[PublicAPI]
	public static class MathH {
		public const float Sqrt2 = 1.41421356237f;
		public const float Sqrt3 = 1.73205080757f;
		public const float HalfSqrt3 = Sqrt3 / 2f;

		[Pure] public static int Floor(float value) => (int)MathF.Floor(value);
		[Pure] public static long Floor(double value) => (long)System.Math.Floor(value);
		[Pure] public static int Floor(decimal value) => (int)System.Math.Floor(value);

		[Pure] public static int Ceil(float value) => (int)MathF.Ceiling(value);
		[Pure] public static long Ceil(double value) => (long)System.Math.Ceiling(value);
		[Pure] public static int Ceil(decimal value) => (int)System.Math.Ceiling(value);

		[Pure] public static int Round(float value) => (int)MathF.Round(value);
		[Pure] public static long Round(double value) => (long)System.Math.Round(value);
		[Pure] public static int Round(decimal value) => (int)System.Math.Round(value);
		[Pure] public static int Round(float value, MidpointRounding rounding) => (int)MathF.Round(value, rounding);
		[Pure] public static long Round(double value, MidpointRounding rounding) => (long)System.Math.Round(value, rounding);
		[Pure] public static int Round(decimal value, MidpointRounding rounding) => (int)System.Math.Round(value, rounding);
	}
}