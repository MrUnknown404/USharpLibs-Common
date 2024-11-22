namespace USharpLibs.Common.Math {
	[PublicAPI]
	public static class MathH {
		public const float Sqrt2 = 1.41421356237f;
		public const float Sqrt3 = 1.73205080757f;
		public const float HalfSqrt3 = Sqrt3 / 2f;

		[Pure] public static double ToRadians(double degrees) => degrees * System.Math.PI / 180d;
		[Pure] public static double ToDegrees(double rads) => rads * 180d / System.Math.PI;
		[Pure] public static float ToRadians(float degrees) => degrees * MathF.PI / 180f;
		[Pure] public static float ToDegrees(float rads) => rads * 180f / MathF.PI;

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

		[Pure] public static float Lerp(float from, float to, float alpha) => from * (1f - alpha) + to * alpha;
		[Pure] public static double Lerp(double from, double to, double alpha) => from * (1d - alpha) + to * alpha;

		[Pure] public static ushort BytesToUShort(byte byte0, byte byte1) => (ushort)((byte0 << 8) + byte1);
		[Pure] public static short BytesToShort(byte byte0, byte byte1) => (short)((byte0 << 8) + byte1);

		[Pure]
		public static void UShortToBytes(ushort value, out byte byte0, out byte byte1) {
			byte0 = (byte)((value >> 8) & byte.MaxValue);
			byte1 = (byte)(value & byte.MaxValue);
		}

		[Pure]
		public static void ShortToBytes(short value, out byte byte0, out byte byte1) {
			byte0 = (byte)((value >> 8) & byte.MaxValue);
			byte1 = (byte)(value & byte.MaxValue);
		}

		[Pure] public static uint BytesToUInt(byte byte0, byte byte1, byte byte2, byte byte3) => (uint)((byte0 << 24) + (byte1 << 16) + (byte2 << 8) + byte3);
		[Pure] public static int BytesToInt(byte byte0, byte byte1, byte byte2, byte byte3) => (byte0 << 24) + (byte1 << 16) + (byte2 << 8) + byte3;

		[Pure]
		public static void UIntToBytes(uint value, out byte byte0, out byte byte1, out byte byte2, out byte byte3) {
			byte0 = (byte)(value >> 24);
			byte1 = (byte)(value >> 16);
			byte2 = (byte)(value >> 8);
			byte3 = (byte)value;
		}

		[Pure]
		public static void IntToBytes(int value, out byte byte0, out byte byte1, out byte byte2, out byte byte3) {
			byte0 = (byte)(value >> 24);
			byte1 = (byte)(value >> 16);
			byte2 = (byte)(value >> 8);
			byte3 = (byte)value;
		}
	}
}