using System.Collections;
using JetBrains.Annotations;

namespace USharpLibs.Common.Collections {
	[PublicAPI]
	public class Grid<T> : IEnumerable<T>, IEquatable<Grid<T>> {
		protected T?[] RawGrid { get; }
		public uint Width { get; protected set; }
		public uint Height { get; protected set; }

		public int Count => RawGrid.Length;
		public bool IsEmpty => RawGrid.Length == 0;

		public Grid(T? @default, uint width, uint height) {
			RawGrid = new T?[width * height];
			Width = width;
			Height = height;

			for (int i = 0; i < width * height; i++) { RawGrid[i] = @default; }
		}

		public Grid(uint width, uint height) : this(default, width, height) { }

		public T? this[uint x, uint y] {
			get {
				if (x >= Width) { throw new ArgumentException($"X cannot be above or equal to Width. Was {x}/{Width}"); }
				if (y >= Height) { throw new ArgumentException($"Y cannot be above or equal to Height. Was {y}/{Height}"); }

				return RawGrid[x + y * Width];
			}
			set {
				if (x >= Width) { throw new ArgumentException($"X cannot be above or equal to Width. Was {x}/{Width}"); }
				if (y >= Height) { throw new ArgumentException($"Y cannot be above or equal to Height. Was {y}/{Height}"); }

				RawGrid[x + y * Width] = value;
			}
		}

		public static bool operator ==(Grid<T>? left, Grid<T>? right) => Equals(left, right);
		public static bool operator !=(Grid<T>? left, Grid<T>? right) => !Equals(left, right);

		public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)RawGrid.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public bool Equals(Grid<T>? other) {
			if (ReferenceEquals(null, other)) { return false; }
			if (ReferenceEquals(this, other)) { return true; }

			if (Width != other.Width || Height != other.Height) { return false; }

			for (int i = 0; i < RawGrid.Length; i++) {
				T? o0 = RawGrid[i], o1 = other.RawGrid[i];
				if ((o0 == null && o1 != null) || (o0 != null && !o0.Equals(o1))) { return false; }
			}

			return true;
		}

		public override bool Equals(object? obj) {
			if (ReferenceEquals(null, obj)) { return false; }
			if (ReferenceEquals(this, obj)) { return true; }
			return obj.GetType() == GetType() && Equals((Grid<T>)obj);
		}

		public override int GetHashCode() => HashCode.Combine(Width, Height);
	}
}