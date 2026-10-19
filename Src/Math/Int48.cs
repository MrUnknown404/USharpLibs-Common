namespace USharpLibs.Common.Math;

public readonly record struct Int48 : IComparable<Int48> {
	public const long MinValue = 0x800000000000;
	public const long MaxValue = 0x7FFFFFFFFFFF;

	public static Int48 Zero => new(0);
	public static Int48 One => new(1);

	private readonly byte byte0;
	private readonly byte byte1;
	private readonly byte byte2;
	private readonly byte byte3;
	private readonly byte byte4;
	private readonly byte byte5;

	public long Value => byte0 | ((long)byte1 << 8) | ((long)byte2 << 16) | ((long)byte3 << 24) | ((long)byte4 << 32) | ((long)byte5 << 40);

	public Int48(long value) {
		byte0 = (byte)((value >> 0) & 0xFF);
		byte1 = (byte)((value >> 8) & 0xFF);
		byte2 = (byte)((value >> 16) & 0xFF);
		byte3 = (byte)((value >> 24) & 0xFF);
		byte4 = (byte)((value >> 32) & 0xFF);
		byte5 = (byte)((value >> 40) & 0xFF);
	}

	public static implicit operator Int48(long value) => new(value);

	public static Int48 operator +(Int48 left, Int48 right) => new(left.Value + right.Value);
	public static Int48 operator -(Int48 left, Int48 right) => new(left.Value - right.Value);
	public static Int48 operator *(Int48 left, Int48 right) => new(left.Value * right.Value);
	public static Int48 operator /(Int48 left, Int48 right) => new(left.Value / right.Value);

	public static Int48 operator ++(Int48 value) => new(value.Value + 1);
	public static Int48 operator --(Int48 value) => new(value.Value - 1);

	public int CompareTo(Int48 other) {
		int comparison = byte0.CompareTo(other.byte0);
		if (comparison != 0) { return comparison; }

		comparison = byte1.CompareTo(other.byte1);
		return comparison != 0 ? comparison : byte2.CompareTo(other.byte2);
	}

	public static bool operator <(Int48 left, Int48 right) => left.CompareTo(right) < 0;
	public static bool operator >(Int48 left, Int48 right) => left.CompareTo(right) > 0;

	public static bool operator <=(Int48 left, Int48 right) => left.CompareTo(right) <= 0;
	public static bool operator >=(Int48 left, Int48 right) => left.CompareTo(right) >= 0;
}