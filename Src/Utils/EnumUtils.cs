namespace USharpLibs.Common.Utils {
	public static class EnumUtils {
		public static T[] Values<T>() where T : struct, Enum {
			Type type = typeof(T);
			if (!type.IsEnum) { throw new Exception($"Type '{type}' is not an enum"); }
			return (T[])Enum.GetValues(type);
		}

		public static int Count<T>() where T : struct, Enum {
			Type type = typeof(T);
			if (!type.IsEnum) { throw new Exception($"Type '{type}' is not an enum"); }
			return Enum.GetNames(type).Length;
		}
	}
}