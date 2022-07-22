using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace USharpLibs.Common.Utils {
	public sealed class Logger {
		public const string None = "";
		public const string Reset = "\u001B[0m";
		public const string Black = "\u001B[30m";
		public const string Red = "\u001B[31m";
		public const string Green = "\u001B[32m";
		public const string Yellow = "\u001B[33m";
		public const string Blue = "\u001B[34m";
		public const string Purple = "\u001B[35m";
		public const string Cyan = "\u001B[36m";
		public const string White = "\u001B[37m";

		public static readonly Logger MinimalLogger = new();
		public static readonly Logger DefaultLogger = new(LogLevel.Normal, "default");

		private readonly string source = string.Empty;
		private readonly LogLevel level = LogLevel.Minimal;

		public Logger() { }

		private Logger(LogLevel level, string source) {
			this.level = level;
			this.source = source;
		}

		public static Logger Normal(string source = "") => new(LogLevel.Normal, source);
		public static Logger More(string source = "") => new(LogLevel.More, source);
		public static Logger Maximum(string source = "") => new(LogLevel.Maximum, source);

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void WriteLine(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.WriteLine(Format(message, None, method, line));
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void WarnLine(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.WriteLine(Format(message, Yellow, method, line));
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ErrorLine(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.Error.WriteLine(Format(message, Red, method, line));

		[MethodImpl(MethodImplOptions.NoInlining)]
		private string Format(string message, string color, string method, int line) {
			string start = $"{color}[{DateTime.Now:HH:mm:ss:fff}]";

			return level switch {
				LogLevel.Minimal => $"{start} {message}",
				LogLevel.Normal => $"{start} [{(!string.IsNullOrWhiteSpace(source) ? $"{source}/" : string.Empty)}{method}.{(line == int.MinValue ? "???" : line)}] {message}",
				LogLevel.More => $"{start} [{(!string.IsNullOrWhiteSpace(source) ? $"{source}/" : string.Empty)}{new StackFrame(2, false).GetMethod()?.DeclaringType?.Name}.{method}.{(line == int.MinValue ? "???" : line)}] {message}",
				LogLevel.Maximum => $"{start} [{(!string.IsNullOrWhiteSpace(source) ? $"{source}/" : string.Empty)}{new StackFrame(2, false).GetMethod()?.DeclaringType?.FullName}.{method}.{(line == int.MinValue ? "???" : line)}] {message}",
				_ => throw new NotImplementedException(),
			};
		}
	}

	public enum LogLevel {
		Minimal = 0,
		Normal,
		More,
		Maximum,
	}
}