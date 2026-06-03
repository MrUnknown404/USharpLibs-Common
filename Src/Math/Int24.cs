namespace USharpLibs.Common.Math;

public readonly record struct Int24 : IComparable<Int24> {
	public const int MinValue = 0x800000;
	public const int MaxValue = 0x7FFFFF;

	public static Int24 One => new(0);
	public static Int24 Zero => new(0);

	private readonly byte byte0;
	private readonly byte byte1;
	private readonly byte byte2;

	public int Value => byte0 | (byte1 << 8) | (byte2 << 16);

	public Int24(int value) {
		byte0 = (byte)((value >> 0) & 0xFF);
		byte1 = (byte)((value >> 8) & 0xFF);
		byte2 = (byte)((value >> 16) & 0xFF);
	}

	public static implicit operator Int24(int value) => new(value);

	// public static UInt24 operator +(UInt24 left, UInt24 right) => new(left.Value + right.Value);
	public static Int24 operator -(Int24 left, Int24 right) => new(left.Value - right.Value);
	public static Int24 operator *(Int24 left, Int24 right) => new(left.Value * right.Value);
	public static Int24 operator /(Int24 left, Int24 right) => new(left.Value / right.Value);

	public static Int24 operator ++(Int24 value) => new(value.Value + 1);
	public static Int24 operator --(Int24 value) => new(value.Value - 1);

	public int CompareTo(Int24 other) {
		int comparison = byte0.CompareTo(other.byte0);
		if (comparison != 0) { return comparison; }

		comparison = byte1.CompareTo(other.byte1);
		return comparison != 0 ? comparison : byte2.CompareTo(other.byte2);
	}

	public static bool operator <(Int24 left, Int24 right) => left.CompareTo(right) < 0;
	public static bool operator >(Int24 left, Int24 right) => left.CompareTo(right) > 0;

	public static bool operator <=(Int24 left, Int24 right) => left.CompareTo(right) <= 0;
	public static bool operator >=(Int24 left, Int24 right) => left.CompareTo(right) >= 0;
}