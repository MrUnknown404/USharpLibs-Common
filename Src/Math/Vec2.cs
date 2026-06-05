using System.Numerics;

namespace USharpLibs.Common.Math;

public readonly record struct Vec2<T> : IComparable<Vec2<T>> where T : INumber<T> {
	public T X { get; init; }
	public T Y { get; init; }

	public Vec2(T x, T y) {
		X = x;
		Y = y;
	}

	public int CompareTo(Vec2<T> other) {
		int xComparison = X.CompareTo(other.X);
		return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
	}

	public override string ToString() => $"{X}, {Y}";

	public static Vec2<T> operator +(Vec2<T> left, Vec2<T> right) => new(left.X + right.X, left.Y + right.Y);
	public static Vec2<T> operator -(Vec2<T> left, Vec2<T> right) => new(left.X - right.X, left.Y - right.Y);
	public static Vec2<T> operator *(Vec2<T> left, Vec2<T> right) => new(left.X * right.X, left.Y * right.Y);
	public static Vec2<T> operator /(Vec2<T> left, Vec2<T> right) => new(left.X / right.X, left.Y / right.Y);

	public static Vec2<T> operator +(Vec2<T> left, T right) => new(left.X + right, left.Y + right);
	public static Vec2<T> operator -(Vec2<T> left, T right) => new(left.X - right, left.Y - right);
	public static Vec2<T> operator *(Vec2<T> left, T right) => new(left.X * right, left.Y * right);
	public static Vec2<T> operator /(Vec2<T> left, T right) => new(left.X / right, left.Y / right);

	public static bool operator <(Vec2<T> left, Vec2<T> right) => left.CompareTo(right) < 0;
	public static bool operator >(Vec2<T> left, Vec2<T> right) => left.CompareTo(right) > 0;
	public static bool operator <=(Vec2<T> left, Vec2<T> right) => left.CompareTo(right) <= 0;
	public static bool operator >=(Vec2<T> left, Vec2<T> right) => left.CompareTo(right) >= 0;
}