using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class Logger {
		public static LogLevel LogLevel { get; set; } = LogLevel.Normal;

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Info(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.WriteLine(Format(message, WarningLevel.Info, method, line));

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Debug(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.WriteLine(Format(message, WarningLevel.Debug, method, line));

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Warn(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.WriteLine(Format(message, WarningLevel.Warning, method, line));

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Error(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.WriteLine(Format(message, WarningLevel.Error, method, line));

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Fatal(string message, [CallerMemberName] string method = "???", [CallerLineNumber] int line = int.MinValue) => Console.WriteLine(Format(message, WarningLevel.Fatal, method, line));

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void PrintException(Exception e) {
			Exception inner = e;
			while (true) {
				if (inner.InnerException != null) { inner = inner.InnerException; } else { break; }
			}

			if (LogLevel == LogLevel.Minimal) {
				Console.Error.WriteLine($"[{DateTime.Now:HH:mm:ss:fff}] {inner.GetType().Name}: {inner.Message}");
				return;
			}

			StackFrame frame = new StackTrace(2, true).GetFrame(0) ?? throw new Exception();
			Console.Error.WriteLine(LogLevel == LogLevel.Maximum
					? $"[{DateTime.Now:HH:mm:ss:fff}] [{WarningLevel.Error}] [{GetMethodName(frame.GetMethod()?.ReflectedType?.FullName ?? throw new Exception())}.{frame.GetFileLineNumber()}] {inner.GetType().Name}: {inner.Message}"
					: $"[{DateTime.Now:HH:mm:ss:fff}] [{WarningLevel.Error}] [{GetMethodName(frame.GetMethod()?.Name ?? throw new Exception())}.{frame.GetFileLineNumber()}] {inner.GetType().Name}: {inner.Message}");
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string Format(string message, WarningLevel warningLevel, string method, int line) =>
				LogLevel switch {
						LogLevel.Minimal => $"[{DateTime.Now:HH:mm:ss:fff}] {message}",
						LogLevel.Maximum =>
								$"[{DateTime.Now:HH:mm:ss:fff}] [{warningLevel}] [{(new StackTrace(2, false).GetFrame(0)?.GetMethod()?.ReflectedType?.FullName ?? throw new Exception()).Replace("+<>c", string.Empty)}.{line}] {message}",
						_ => $"[{DateTime.Now:HH:mm:ss:fff}] [{warningLevel}] [{GetMethodName(method)}.{line}] {message}",
				};

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string GetMethodName(string method) {
			if (method is ".ctor" or ".cctor" || LogLevel > LogLevel.Normal) {
				string @namespace = new StackTrace(3, false).GetFrame(0)?.GetMethod()?.ReflectedType?.FullName ?? throw new Exception();
				return $"{@namespace[(@namespace.LastIndexOf('.') + 1)..].Replace("+<>c", string.Empty)}.{method}";
			}

			return method;
		}

		public static void SetupDefaultLogFolder(ushort maxAmountOfLogs, string startupMessage = "") => SetupDefaultLogFolder(maxAmountOfLogs, null, startupMessage);

		public static void SetupDefaultLogFolder(ushort maxAmountOfLogs, CreateNewOutput? createNewOutput, string startupMessage = "") {
			const string LogDateFormat = "MM-dd-yyyy HH-mm-ss-fff";

			string logsDirName = Directory.CreateDirectory("Logs").FullName;
			LoggerWriter newOut = createNewOutput?.Invoke(Console.Out, new($"Logs\\{DateTime.Now.ToString(LogDateFormat)}.log", FileMode.Create)) ?? new(Console.Out,
					new($"Logs\\{DateTime.Now.ToString(LogDateFormat)}.log", FileMode.Create));

			Console.SetOut(newOut);
			Console.SetError(newOut);
			AppDomain.CurrentDomain.UnhandledException += (_, args) => PrintException((Exception)args.ExceptionObject);

			if (startupMessage.Length != 0) { Info(startupMessage); }
			Debug($"Logs -> {logsDirName}");

			List<DateTime> dates = new();
			foreach (string f in Directory.GetFiles("Logs")) {
				if (f.EndsWith(".log") && f.Length == 32 && DateTime.TryParseExact(f[5..^4], LogDateFormat, null, DateTimeStyles.None, out DateTime time)) { dates.Add(time); }
			}

			if (dates.Count > maxAmountOfLogs) {
				dates.Sort(DateTime.Compare);

				Debug("Found too many log files. Deleting the oldest");
				while (dates.Count > maxAmountOfLogs) {
					File.Delete($"Logs\\{dates[0].ToString(LogDateFormat)}.log");
					dates.RemoveAt(0);
				}
			}
		}

		public delegate LoggerWriter CreateNewOutput(TextWriter oldOut, FileStream fileStream);
	}

	[PublicAPI]
	public class LoggerWriter : StreamWriter {
		public override Encoding Encoding => Encoding.UTF8;
		private readonly TextWriter old;

		public LoggerWriter(TextWriter old, FileStream file) : base(file) {
			this.old = old;
			// ReSharper disable once VirtualMemberCallInConstructor
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

	public enum WarningLevel {
		Debug = 0,
		Info,
		Warning,
		Error,
		Fatal,
	}
}