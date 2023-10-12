using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace USharpLibs.Common.Math {
	[PublicAPI]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Bit : IEquatable<Bit>, IComparable<Bit> {
		private bool Value { get; }

		private static Bit Zero => 0;
		private static Bit One => 1;
		private static Bit False => Zero;
		private static Bit True => One;

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

		public static bool operator ==(Bit left, Bit right) => left.Equals(right);
		public static bool operator !=(Bit left, Bit right) => !left.Equals(right);

		public bool Equals(Bit other) => Value == other.Value;
		public int CompareTo(Bit other) => Value.CompareTo(other.Value);

		public override bool Equals(object? obj) => obj is Bit other && Equals(other);
		public override int GetHashCode() => Value.GetHashCode();

		public override string ToString() => Value.ToString();
	}
}