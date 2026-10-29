using System.Numerics;
using JetBrains.Annotations;

namespace USharpLibs.Common.Math;

[PublicAPI]
public readonly record struct Vec3<T> where T : INumber<T> {
	public T X { get; init; }
	public T Y { get; init; }
	public T Z { get; init; }

	public Vec3(T x, T y, T z) {
		X = x;
		Y = y;
		Z = z;
	}

	public Vec3(Vec2<T> vec, T z) : this(vec.X, vec.Y, z) { }
	public Vec3(T value) : this(value, value, value) { }

	public void Deconstruct(out T x, out T y, out T z) {
		x = X;
		y = Y;
		z = Z;
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

	public static Vec3<T> operator -(Vec3<T> self) => new(-self.X, -self.Y, -self.Z);
}