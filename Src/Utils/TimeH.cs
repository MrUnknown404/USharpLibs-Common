using System.Diagnostics;

namespace USharpLibs.Common.Utils {
	public static class TimeH {
		public static TimeSpan Time(Action run) {
			Stopwatch w = new();
			w.Start();
			run();
			w.Stop();
			return w.Elapsed;
		}
	}
}