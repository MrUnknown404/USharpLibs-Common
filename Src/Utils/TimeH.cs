using System.Diagnostics;
using JetBrains.Annotations;
using USharpLibs.Common.IO;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
	public static class TimeH {
		private static Stopwatch Stopwatch { get; } = new();

		public static TimeSpan Time(Action run) {
			Stopwatch w = new();
			w.Start();
			run();
			w.Stop();
			return w.Elapsed;
		}

		public static void Start() => Stopwatch.Restart();

		[MustUseReturnValue]
		public static TimeSpan Stop() {
			if (!Stopwatch.IsRunning) {
				Logger.Warn("Timer is not running.");
				return TimeSpan.Zero;
			}

			Stopwatch.Stop();
			return Stopwatch.Elapsed;
		}
	}
}