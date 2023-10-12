using JetBrains.Annotations;

namespace USharpLibs.Common.Collections {
	[PublicAPI]
	public class Grid<T> {
		public List<T?> RawGrid { get; protected set; }
		public int Width { get; protected set; }
		public int Height { get; protected set; }

		public Grid(T? @default, int width, int height) {
			RawGrid = new(width * height);
			Width = width;
			Height = height;

			for (int i = 0; i < width * height; i++) { RawGrid.Add(@default); }
		}

		public Grid(int width, int height) : this(default, width, height) { }

		public bool Is(Grid<T?> grid) {
			if (grid.Width != Width || grid.Height != Height) { return false; }

			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					T? g0 = this[x, y], g1 = grid[x, y];
					if ((g0 == null && g1 != null) || (g0 != null && !g0.Equals(g1))) { return false; }
				}
			}

			return true;
		}

		public T? this[int x, int y] { get => RawGrid[x + y * Width]; set => RawGrid[x + y * Width] = value; }

		public int Count() => RawGrid.Count;
		public bool IsEmpty() => RawGrid.Count == 0;
	}
}