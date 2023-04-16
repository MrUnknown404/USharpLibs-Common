using System.Diagnostics;
using JetBrains.Annotations;

namespace USharpLibs.Common.Utils {
	[PublicAPI]
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