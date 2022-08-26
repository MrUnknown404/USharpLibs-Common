using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

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
		public void PrintException(Exception e) {
			Exception inner = e;
			while (true) {
				if (inner.InnerException != null) {
					inner = inner.InnerException;
				} else { break; }
			}

			if (level == LogLevel.Minimal) {
				Console.Error.WriteLine($"{GetStart(Red)} {inner.GetType().Name}: {inner.Message}");
				return;
			}

			StackTrace trace = new(2, true);
			StackFrame frame = trace.GetFrame(0) ?? throw new Exception();
			string method = frame.GetMethod()?.Name ?? throw new Exception();

			if (method == "<>c") { method = GetNamespace(trace.GetFrame(1) ?? throw new Exception()); }
			if (method.Contains(".ctor")) { method = ".ctor"; }

			if (level == LogLevel.Normal) {
				Console.Error.WriteLine($"{GetStart(Red)} [{GetSource()}{method}.{GetLineNumber(frame.GetFileLineNumber())}] {inner.GetType().Name}: {inner.Message}");
				return;
			}

			string @namespace = GetNamespace(frame);
			if (@namespace == "<>c") { @namespace = GetNamespace(trace.GetFrame(1) ?? throw new Exception()); }

			Console.Error.WriteLine($"{GetStart(Red)} [{GetSource()}{@namespace.Replace("+<>c", string.Empty)}.{method}.{GetLineNumber(frame.GetFileLineNumber())}] {inner.GetType().Name}: {inner.Message}");
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private string Format(string message, string color, string method, int line) {
			if (level == LogLevel.Minimal) {
				return $"{GetStart(color)} {message}";
			} else {
				if (level == LogLevel.Normal) { return $"{GetStart(color)} [{GetSource()}{method}.{GetLineNumber(line)}] {message}"; }

				StackTrace trace = new(2, false);
				StackFrame frame = trace.GetFrame(0) ?? throw new Exception();

				string @namespace = GetNamespace(frame);
				if (@namespace == "<>c") { @namespace = GetNamespace(trace.GetFrame(1) ?? throw new Exception()); }

				return $"{GetStart(color)} [{GetSource()}{@namespace.Replace("+<>c", string.Empty)}.{method}.{GetLineNumber(line)}] {message}";
			}
		}

		private static string GetStart(string color) => $"{color}[{DateTime.Now:HH:mm:ss:fff}]";
		private string GetNamespace(StackFrame frame) => level == LogLevel.More ? frame.GetMethod()?.DeclaringType?.Name ?? "???" : level == LogLevel.Maximum ? frame.GetMethod()?.DeclaringType?.FullName ?? "???" : throw new Exception($"Unknown logging level: {level}");
		private string GetSource() => !string.IsNullOrWhiteSpace(source) ? $"{source}/" : string.Empty;
		private static string GetLineNumber(int value) => value <= 0 ? "???" : value.ToString();
	}

	public class LoggerWriter : StreamWriter {
		public override Encoding Encoding => Encoding.UTF8;
		private readonly TextWriter old;

		public LoggerWriter(TextWriter old, FileStream file) : base(file) {
			this.old = old;
			AutoFlush = true;
		}

		public override void Write(ReadOnlySpan<char> buffer) {
			base.Write(buffer);
			old.Write(buffer);
		}

		public override void Write(StringBuilder? value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(bool value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(char value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(char[] buffer, int index, int count) {
			base.Write(buffer, index, count);
			old.Write(buffer, index, count);
		}

		public override void Write(char[]? buffer) {
			base.Write(buffer);
			old.Write(buffer);
		}

		public override void Write(decimal value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(double value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(float value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(int value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(long value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(object? value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(string format, object? arg0) {
			base.Write(format, arg0);
			old.Write(format, arg0);
		}

		public override void Write(string format, object? arg0, object? arg1) {
			base.Write(format, arg0, arg1);
			old.Write(format, arg0, arg1);
		}

		public override void Write(string format, object? arg0, object? arg1, object? arg2) {
			base.Write(format, arg0, arg1, arg2);
			old.Write(format, arg0, arg1, arg2);
		}

		public override void Write(string format, params object?[] arg) {
			base.Write(format, arg);
			old.Write(format, arg);
		}

		public override void Write(string? value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(uint value) {
			base.Write(value);
			old.Write(value);
		}

		public override void Write(ulong value) {
			base.Write(value);
			old.Write(value);
		}

		public override void WriteLine() {
			base.WriteLine();
			old.WriteLine();
		}

		public override void WriteLine(ReadOnlySpan<char> buffer) {
			base.WriteLine(buffer);
			old.WriteLine(buffer);
		}

		public override void WriteLine(StringBuilder? value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(bool value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(char value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(char[] buffer, int index, int count) {
			base.WriteLine(buffer, index, count);
			old.WriteLine(buffer, index, count);
		}

		public override void WriteLine(char[]? buffer) {
			base.WriteLine(buffer);
			old.WriteLine(buffer);
		}

		public override void WriteLine(decimal value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(double value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(float value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(int value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(long value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(object? value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(string format, object? arg0) {
			base.WriteLine(format, arg0);
			old.WriteLine(format, arg0);
		}

		public override void WriteLine(string format, object? arg0, object? arg1) {
			base.WriteLine(format, arg0, arg1);
			old.WriteLine(format, arg0, arg1);
		}

		public override void WriteLine(string format, object? arg0, object? arg1, object? arg2) {
			base.WriteLine(format, arg0, arg1, arg2);
			old.WriteLine(format, arg0, arg1, arg2);
		}

		public override void WriteLine(string format, params object?[] arg) {
			base.WriteLine(format, arg);
			old.WriteLine(format, arg);
		}

		public override void WriteLine(string? value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(uint value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}

		public override void WriteLine(ulong value) {
			base.WriteLine(value);
			old.WriteLine(value);
		}
	}

	public enum LogLevel {
		Minimal = 0,
		Normal,
		More,
		Maximum,
	}
}