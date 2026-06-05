using System.Numerics;

namespace USharpLibs.Common.Math;

public readonly record struct Vec3<T> : IComparable<Vec3<T>> where T : INumber<T> {
	public T X { get; init; }
	public T Y { get; init; }
	public T Z { get; init; }

	public Vec3(T x, T y, T z) {
		X = x;
		Y = y;
		Z = z;
	}

	public Vec3(Vec2<T> vec, T z) : this(vec.X, vec.Y, z) { }

	public int CompareTo(Vec3<T> other) {
		int xComparison = X.CompareTo(other.X);
		if (xComparison != 0) { return xComparison; }
		int yComparison = Y.CompareTo(other.Y);
		return yComparison != 0 ? yComparison : Z.CompareTo(other.Z);
	}

	public override string ToString() => $"{X}, {Y}, {Z}";

	public static Vec3<T> operator +(Vec3<T> left, Vec3<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	public static Vec3<T> operator -(Vec3<T> left, Vec3<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	public static Vec3<T> operator *(Vec3<T> left, Vec3<T> right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
	public static Vec3<T> operator /(Vec3<T> left, Vec3<T> right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

	public static Vec3<T> operator +(Vec3<T> left, T right) => new(left.X + right, left.Y + right, left.Z + right);
	public static Vec3<T> operator -(Vec3<T> left, T right) => new(left.X - right, left.Y - right, left.Z - right);
	public static Vec3<T> operator *(Vec3<T> left, T right) => new(left.X * right, left.Y * right, left.Z * right);
	public static Vec3<T> operator /(Vec3<T> left, T right) => new(left.X / right, left.Y / right, left.Z / right);

	public static bool operator <(Vec3<T> left, Vec3<T> right) => left.CompareTo(right) < 0;
	public static bool operator >(Vec3<T> left, Vec3<T> right) => left.CompareTo(right) > 0;
	public static bool operator <=(Vec3<T> left, Vec3<T> right) => left.CompareTo(right) <= 0;
	public static bool operator >=(Vec3<T> left, Vec3<T> right) => left.CompareTo(right) >= 0;
}