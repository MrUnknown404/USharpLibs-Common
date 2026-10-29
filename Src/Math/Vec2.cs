using System.Numerics;
using JetBrains.Annotations;

namespace USharpLibs.Common.Math;

[PublicAPI]
public readonly record struct Vec2<T> where T : INumber<T> {
	public T X { get; init; }
	public T Y { get; init; }

	public Vec2(T x, T y) {
		X = x;
		Y = y;
	}

	public Vec2(T value) : this(value, value) { }

	public void Deconstruct(out T x, out T y) {
		x = X;
		y = Y;
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

	public static Vec2<T> operator -(Vec2<T> self) => new(-self.X, -self.Y);
}