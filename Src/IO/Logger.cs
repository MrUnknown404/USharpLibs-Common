using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace USharpLibs.Common.IO {
	/// <summary>
	/// This is Attempt #3 at a decent logging utility class. So far, this one works the best. <br/>
	/// Call <see cref="Init(string)"/> to enable extra logging.
	/// </summary>
	/// <remarks> Check the class's static variables for settings. </remarks>
	[PublicAPI]
	public static class Logger {
		private static bool createLogFile = true;
		private static ushort maxLogFiles = 5;
		private static string logDirectory = string.Empty;

		/// <summary> Whether or not the logger should automatically create log files. </summary>
		public static bool CreateLogFile {
			private get => createLogFile;
			set {
				if (wasInitRun) { Warn("Updating CreateLogFile after #Init was called will do nothing."); }
				createLogFile = value;
			}
		}

		/// <summary> The amount of log files to keep if <see cref="CreateLogFile"/> is true. If false, this does nothing. </summary>
		public static ushort MaxLogFiles {
			private get => maxLogFiles;
			set {
				if (wasInitRun) { Warn("Updating MaxLogFiles after #Init was called will do nothing."); }
				maxLogFiles = value;
			}
		}

		/// <summary> The absolute directory for the logs folder. This is only generated if <see cref="CreateLogFile"/> is true. If false, this will not be null, only empty. </summary>
		public static string LogDirectory {
			get {
				if (!wasInitRun) { Warn("LogDirectory cannot be called for before #Init is called."); }
				return logDirectory;
			}
			private set => logDirectory = value;
		}

		/// <summary> Whether or not to include the current time in the formatted log prefix. </summary>
		public static bool PrintTimeStamp { private get; set; } = true;
		/// <summary> Whether or not to include the severity level in the formatted log prefix. </summary>
		public static bool PrintSeverity { private get; set; } = true;
		/// <summary> Whether or not to include the thread name in the formatted log prefix. </summary>
		public static bool PrintThreadName { private get; set; }

		/// <summary> Whether or not to include the namespace in the formatted log prefix. </summary>
		public static bool PrintNamespace { private get; set; }
		/// <summary> Whether or not to include the class name in the formatted log prefix. </summary>
		public static bool PrintClass { private get; set; } = true;
		/// <summary> Whether or not to include the method name in the formatted log prefix. </summary>
		public static bool PrintMethod { private get; set; } = true;
		/// <summary> Whether or not to include the line number in the formatted log prefix. </summary>
		public static bool PrintLine { private get; set; } = true;

		private static LoggerWritter? loggerWritter;
		private static bool wasInitRun;

		public static void Init(string startingMessage = "") {
			const string LogDateFormat = "MM-dd-yyyy HH-mm-ss-fff";

			if (CreateLogFile) { LogDirectory = Directory.CreateDirectory("Logs").FullName; }

			loggerWritter = new(Console.Out, new FileStream($"Logs/{DateTime.Now.ToString(LogDateFormat)}.log", FileMode.Create));
			Console.SetOut(loggerWritter);
			Console.SetError(loggerWritter);
			AppDomain.CurrentDomain.UnhandledException += (_, args) => PrintException((Exception)args.ExceptionObject, 5);

			if (!string.IsNullOrEmpty(startingMessage)) { Info(startingMessage); }

			if (CreateLogFile) {
				List<DateTime> dates = new();
				foreach (string f in Directory.GetFiles("Logs")) {
					if (f.EndsWith(".log") && f.Length == 32 && DateTime.TryParseExact(f[5..^4], LogDateFormat, null, DateTimeStyles.None, out DateTime time)) { dates.Add(time); }
				}

				if (dates.Count > MaxLogFiles) {
					dates.Sort(DateTime.Compare);

					Debug("Found too many log files. Deleting the oldest");
					while (dates.Count > MaxLogFiles) {
						File.Delete($"Logs\\{dates[0].ToString(LogDateFormat)}.log");
						dates.RemoveAt(0);
					}
				}
			}

			wasInitRun = true;
		}

		// Most of this below could be simplified. but idc

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void PrintException(Exception e) => PrintException(e, 5);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void PrintException(Exception e, byte stackTraceLevel) {
			Exception inner = e;
			while (true) {
				if (inner.InnerException != null) { inner = inner.InnerException; } else { break; }
			}

			// TODO make my own cleaner version of StackTrace#ToString
			Error($"{inner.GetType().Name}: {inner.Message} \n{new StackTrace(1, true)}", stackTraceLevel);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Debug(object? obj) => Debug(obj?.ToString(), 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Debug(string? message) => Debug(message, 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Debug(string? message, byte stackTraceLevel) {
			if (loggerWritter == null) {
				Console.WriteLine(message);
				return;
			}

			loggerWritter.WriteLine(WarningLevel.Debug, message, stackTraceLevel);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Info(object? obj) => Info(obj?.ToString(), 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Info(string? message) => Info(message, 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Info(string? message, byte stackTraceLevel) {
			if (loggerWritter == null) {
				Console.WriteLine(message);
				return;
			}

			loggerWritter.WriteLine(WarningLevel.Info, message, stackTraceLevel);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Warn(object? obj) => Warn(obj?.ToString(), 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Warn(string? message) => Warn(message, 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Warn(string? message, byte stackTraceLevel) {
			if (loggerWritter == null) {
				Console.WriteLine(message);
				return;
			}

			loggerWritter.WriteLine(WarningLevel.Warning, message, stackTraceLevel);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Error(object? obj) => Error(obj?.ToString(), 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Error(string? message) => Error(message, 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Error(string? message, byte stackTraceLevel) {
			if (loggerWritter == null) {
				Console.WriteLine(message);
				return;
			}

			loggerWritter.WriteLine(WarningLevel.Error, message, stackTraceLevel);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Fatal(object? obj) => Fatal(obj?.ToString(), 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Fatal(string? message) => Fatal(message, 4);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Fatal(string? message, byte stackTraceLevel) {
			if (loggerWritter == null) {
				Console.WriteLine(message);
				return;
			}

			loggerWritter.WriteLine(WarningLevel.Fatal, message, stackTraceLevel);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string AddPrefix(WarningLevel level, string? message, byte stackTraceLevel = 5) {
			StringBuilder sb = new();
			if (PrintTimeStamp) { sb.Append($"[{DateTime.Now:HH:mm:ss:fff}] "); }
			if (PrintSeverity) { sb.Append($"[{level}] "); }
			if (PrintThreadName) {
				string? threadName = Thread.CurrentThread.Name;
				sb.Append($"{(string.IsNullOrEmpty(threadName) ? string.Empty : $"[{threadName}] ")}");
			}

			if (PrintNamespace || PrintClass || PrintMethod || PrintLine) {
				StackFrame frame = new StackTrace(stackTraceLevel, PrintLine).GetFrame(0) ?? throw new NullReferenceException();
				MethodBase method = frame.GetMethod() ?? throw new NullReferenceException();
				Type reflectedType = method.ReflectedType ?? throw new NullReferenceException();

				sb.Append('[');

				if (PrintNamespace) { sb.Append(reflectedType.Namespace ?? throw new NullReferenceException()); }

				if (PrintClass) {
					if (PrintNamespace) { sb.Append('.'); }

					string className = reflectedType.Name ?? throw new NullReferenceException();
					if (className.Contains("<>c")) { className = reflectedType.ReflectedType?.Name ?? throw new NullReferenceException(); }
					if (className.Contains('`')) { className = className[..className.IndexOf('`')]; }
					sb.Append(className);
				}

				if (PrintMethod) {
					if (PrintNamespace || PrintClass) { sb.Append('.'); }

					string methodName = method.Name ?? throw new NullReferenceException();
					if (methodName.StartsWith('<')) { methodName = methodName[1..methodName.IndexOf('>')]; }
					if (methodName.Contains(".ctor")) { methodName = methodName.Replace(".ctor", "ctor"); }
					sb.Append(methodName);
				}

				if (PrintLine) {
					if (PrintNamespace || PrintClass || PrintMethod) { sb.Append('.'); }
					sb.Append(frame.GetFileLineNumber());
				}

				sb.Append("] ");
			}

			return sb.Append(message ?? string.Empty).ToString();
		}

		private sealed class LoggerWritter : StreamWriter {
			public override Encoding Encoding => Encoding.UTF8;
			private readonly TextWriter console;

			public LoggerWritter(TextWriter console, Stream file) : base(file) {
				this.console = console;
				AutoFlush = true;
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			public void WriteLine(WarningLevel level, string? value, byte stackTracelevel = 5) {
				string newString = AddPrefix(level, value, stackTracelevel);

				if (CreateLogFile) { base.WriteLine(newString); }
				console.WriteLine(newString);
			}

			// I think I got everything?

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(string? value) => WriteLine(WarningLevel.Debug, value);

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(char value) => WriteLine(WarningLevel.Debug, value.ToString());

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(char[]? buffer) => WriteLine(WarningLevel.Debug, new string(buffer));

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(bool value) => WriteLine(WarningLevel.Debug, value.ToString());

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(float value) => WriteLine(WarningLevel.Debug, value.ToString(CultureInfo.CurrentCulture));

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(double value) => WriteLine(WarningLevel.Debug, value.ToString(CultureInfo.CurrentCulture));

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(decimal value) => WriteLine(WarningLevel.Debug, value.ToString(CultureInfo.CurrentCulture));

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(int value) => WriteLine(WarningLevel.Debug, value.ToString());

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(long value) => WriteLine(WarningLevel.Debug, value.ToString());

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(uint value) => WriteLine(WarningLevel.Debug, value.ToString());

			[MethodImpl(MethodImplOptions.NoInlining)]
			public override void WriteLine(ulong value) => WriteLine(WarningLevel.Debug, value.ToString());
		}
	}

	public enum WarningLevel : byte {
		Debug = 0,
		Info,
		Warning,
		Error,
		Fatal,
	}
}