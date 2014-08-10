
using System;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Mono;
using ScriptCs.Hosting;
using Common.Logging;

namespace ScriptCsHostingTest
{
	public class ScriptCsHost
	{
		ILog logger;
		ScriptServices services;

		public void Run ()
		{
			var console = new ScriptConsole ();
			logger = LogManager.GetLogger (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			var builder = new ScriptServicesBuilder (console, logger, null);
			builder
				.LogLevel (ScriptCs.Contracts.LogLevel.Debug)
				.Repl (false)
				.Cache (false);

			builder.ScriptEngine<ScriptCs.Engine.Mono.MonoScriptEngine> ();
			services = builder.Build ();
			services.Executor.AddReferences (typeof(MyScriptPackContext));
			var scriptPack = new MyScriptPack ();
			services.Executor.Initialize (new string[0], new [] { scriptPack });
		}

		public void ExecuteFile (string fileName)
		{
			try {
				ScriptResult result = services.Executor.Execute (fileName);
				if (result.CompileExceptionInfo != null) {
					logger.Info(result.CompileExceptionInfo.SourceException);
				}
				if (result.ExecuteExceptionInfo != null) {
					logger.Info(result.ExecuteExceptionInfo.SourceException);
				}
			} catch (Exception ex) {
				logger.Error (ex);
			}
		}
	}
}
