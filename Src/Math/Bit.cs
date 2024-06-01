using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace USharpLibs.Common.Math {
	[PublicAPI]
	[StructLayout(LayoutKind.Sequential)]
	public readonly record struct Bit : IComparable<Bit> {
		public static Bit Zero => 0;
		public static Bit One => 1;
		public static Bit False => false;
		public static Bit True => true;

		private bool Value { get; }

		public Bit(bool value) => Value = value;

		public Bit(byte value) : this(value switch {
				1 => true,
				0 => false,
				_ => throw new ArgumentOutOfRangeException(nameof(value)),
		}) { }

		public static implicit operator bool(Bit value) => value.Value;
		public static implicit operator Bit(bool value) => new(value);

		public static implicit operator Bit(byte value) =>
				new(value switch {
						1 => true,
						0 => false,
						_ => throw new ArgumentOutOfRangeException(nameof(value)),
				});

		public static Bit operator |(Bit value1, Bit value2) => new(value1.Value | value2.Value);
		public static Bit operator &(Bit value1, Bit value2) => new(value1.Value & value2.Value);
		public static Bit operator ^(Bit value1, Bit value2) => new(value1.Value ^ value2.Value);
		public static Bit operator ~(Bit value) => new(!value.Value);
		public static Bit operator !(Bit value) => ~value;

		public static bool operator true(Bit value) => value.Value;
		public static bool operator false(Bit value) => value.Value;

		public int CompareTo(Bit other) => Value.CompareTo(other.Value);

		public override int GetHashCode() => Value.GetHashCode();
		public override string ToString() => Value.ToString();
	}
}