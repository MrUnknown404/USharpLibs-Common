using System.Numerics;
using JetBrains.Annotations;

namespace USharpLibs.Common.Math;

[PublicAPI]
public readonly record struct Vec4<T> where T : INumber<T> {
	public T X { get; init; }
	public T Y { get; init; }
	public T Z { get; init; }
	public T W { get; init; }

	public Vec4(T x, T y, T z, T w) {
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	public Vec4(Vec2<T> vec, T z, T w) : this(vec.X, vec.Y, z, w) { }
	public Vec4(Vec2<T> vec0, Vec2<T> vec1) : this(vec0.X, vec0.Y, vec1.X, vec1.Y) { }
	public Vec4(Vec3<T> vec, T w) : this(vec.X, vec.Y, vec.Z, w) { }
	public Vec4(T value) : this(value, value, value, value) { }

	public void Deconstruct(out T x, out T y, out T z, out T w) {
		x = X;
		y = Y;
		z = Z;
		w = W;
	}

	public override string ToString() => $"{X}, {Y}, {Z}, {W}";

	public static Vec4<T> operator +(Vec4<T> left, Vec4<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
	public static Vec4<T> operator -(Vec4<T> left, Vec4<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
	public static Vec4<T> operator *(Vec4<T> left, Vec4<T> right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
	public static Vec4<T> operator /(Vec4<T> left, Vec4<T> right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

	public static Vec4<T> operator +(Vec4<T> left, T right) => new(left.X + right, left.Y + right, left.Z + right, left.W + right);
	public static Vec4<T> operator -(Vec4<T> left, T right) => new(left.X - right, left.Y - right, left.Z - right, left.W - right);
	public static Vec4<T> operator *(Vec4<T> left, T right) => new(left.X * right, left.Y * right, left.Z * right, left.W * right);
	public static Vec4<T> operator /(Vec4<T> left, T right) => new(left.X / right, left.Y / right, left.Z / right, left.W / right);

	public static Vec4<T> operator -(Vec4<T> self) => new(-self.X, -self.Y, -self.Z, -self.W);
}