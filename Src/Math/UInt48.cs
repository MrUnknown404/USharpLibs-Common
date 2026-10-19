namespace USharpLibs.Common.Math;

// TODO IUnsignedNumber<UInt48>, IBinaryInteger<UInt48>
public readonly record struct UInt48 : IComparable<UInt48> {
	public const ulong MinValue = 0;
	public const ulong MaxValue = 0xFFFFFFFFFFFF;

	public static UInt48 Zero => new(0);
	public static UInt48 One => new(1);

	private readonly byte byte0;
	private readonly byte byte1;
	private readonly byte byte2;
	private readonly byte byte3;
	private readonly byte byte4;
	private readonly byte byte5;

	public ulong Value => byte0 | ((ulong)byte1 << 8) | ((ulong)byte2 << 16) | ((ulong)byte3 << 24) | ((ulong)byte4 << 32) | ((ulong)byte5 << 40);

	public UInt48(ulong value) {
		byte0 = (byte)((value >> 0) & 0xFF);
		byte1 = (byte)((value >> 8) & 0xFF);
		byte2 = (byte)((value >> 16) & 0xFF);
		byte3 = (byte)((value >> 24) & 0xFF);
		byte4 = (byte)((value >> 32) & 0xFF);
		byte5 = (byte)((value >> 40) & 0xFF);
	}

	public static implicit operator UInt48(ulong value) => new(value);

	public static UInt48 operator +(UInt48 left, UInt48 right) => new(left.Value + right.Value);
	public static UInt48 operator -(UInt48 left, UInt48 right) => new(left.Value - right.Value);
	public static UInt48 operator *(UInt48 left, UInt48 right) => new(left.Value * right.Value);
	public static UInt48 operator /(UInt48 left, UInt48 right) => new(left.Value / right.Value);

	public static UInt48 operator ++(UInt48 value) => new(value.Value + 1);
	public static UInt48 operator --(UInt48 value) => new(value.Value - 1);

	public int CompareTo(UInt48 other) {
		int comparison = byte0.CompareTo(other.byte0);
		if (comparison != 0) { return comparison; }

		comparison = byte1.CompareTo(other.byte1);
		return comparison != 0 ? comparison : byte2.CompareTo(other.byte2);
	}

	public static bool operator <(UInt48 left, UInt48 right) => left.CompareTo(right) < 0;
	public static bool operator >(UInt48 left, UInt48 right) => left.CompareTo(right) > 0;

	public static bool operator <=(UInt48 left, UInt48 right) => left.CompareTo(right) <= 0;
	public static bool operator >=(UInt48 left, UInt48 right) => left.CompareTo(right) >= 0;
}