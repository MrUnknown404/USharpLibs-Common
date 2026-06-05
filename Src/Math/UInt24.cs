namespace USharpLibs.Common.Math;

// TODO IUnsignedNumber<UInt24>, IBinaryInteger<UInt24>
public readonly record struct UInt24 : IComparable<UInt24> {
	public const uint MinValue = 0;
	public const uint MaxValue = 0xFFFFFF;

	public static UInt24 One => new(0);
	public static UInt24 Zero => new(0);

	private readonly byte byte0;
	private readonly byte byte1;
	private readonly byte byte2;

	public uint Value => (uint)(byte0 | (byte1 << 8) | (byte2 << 16));

	public UInt24(uint value) {
		byte0 = (byte)((value >> 0) & 0xFF);
		byte1 = (byte)((value >> 8) & 0xFF);
		byte2 = (byte)((value >> 16) & 0xFF);
	}

	public static implicit operator UInt24(uint value) => new(value);

	public static UInt24 operator +(UInt24 left, UInt24 right) => new(left.Value + right.Value);
	public static UInt24 operator -(UInt24 left, UInt24 right) => new(left.Value - right.Value);
	public static UInt24 operator *(UInt24 left, UInt24 right) => new(left.Value * right.Value);
	public static UInt24 operator /(UInt24 left, UInt24 right) => new(left.Value / right.Value);

	public static UInt24 operator ++(UInt24 value) => new(value.Value + 1);
	public static UInt24 operator --(UInt24 value) => new(value.Value - 1);

	public int CompareTo(UInt24 other) {
		int comparison = byte0.CompareTo(other.byte0);
		if (comparison != 0) { return comparison; }

		comparison = byte1.CompareTo(other.byte1);
		return comparison != 0 ? comparison : byte2.CompareTo(other.byte2);
	}

	public static bool operator <(UInt24 left, UInt24 right) => left.CompareTo(right) < 0;
	public static bool operator >(UInt24 left, UInt24 right) => left.CompareTo(right) > 0;

	public static bool operator <=(UInt24 left, UInt24 right) => left.CompareTo(right) <= 0;
	public static bool operator >=(UInt24 left, UInt24 right) => left.CompareTo(right) >= 0;
}