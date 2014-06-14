
using System;
using ScriptCs;
using System.IO;

namespace ScriptCsHostingTest
{
	class Program
	{
		string fileName;

		public static void Main (string[] args)
		{
			var program = new Program ();
			program.Run (args);
		}

		void Run (string[] args)
		{
			if (!ValidateArgs (args)) {
				PrintUsage ();
			}

			try {
				var host = new ScriptCsHost ();
				host.Run ();
				host.ExecuteFile (fileName);
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		bool ValidateArgs (string[] args)
		{
			if (args.Length == 1) {
				fileName = args [0];
				if (File.Exists (fileName)) {
					return true;
				} else {
					Console.WriteLine ("Script file does not exist.");
					PrintUsage ();
				}
			}

			return false;
		}

		void PrintUsage ()
		{
			Console.WriteLine ("ScriptCsHostingTest ScriptFileName");
		}
	}
}
