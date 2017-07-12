using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace Jacked_Loader
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FrmMain());
		}
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show(string.Format("Please report the following error in the forums:\n\n{0}", e.ExceptionObject.ToString()), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Process.GetCurrentProcess().Kill();
		}
	}
}
