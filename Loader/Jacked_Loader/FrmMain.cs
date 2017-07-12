using Jacked_Loader.Properties;
using Magic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Jacked_Loader
{
	public class FrmMain : Form
	{
		private DateTime highestBuild = new DateTime(635288524270000000L);
		private Thread loaderThread;
		private BlackMagic memory;
		private IContainer components;
		private Label label1;
		public FrmMain()
		{
			this.InitializeComponent();
		}
		private void frmMain_Load(object sender, EventArgs e)
		{
			this.Text = this.RndString(this.RndNumber(14, 24));
			this.loaderThread = new Thread(new ThreadStart(this.StartInjection))
			{
				IsBackground = true
			};
			this.loaderThread.Start();
		}
		private string DropHack()
		{
			string tempPath = Path.GetTempPath();
			string str = this.RndString(this.RndNumber(16, 24));
			string text = Path.Combine(tempPath, str + ".tmp");
			File.WriteAllBytes(text, HackStub.data);
			File.SetAttributes(text, FileAttributes.Hidden);
            return text;
		}
		private void SetLabelText(Label label, string text)
		{
			base.Invoke(new MethodInvoker(delegate
			{
				label.Text = text;
			}));
		}
		private bool DoesPassSecurityCheck(Process proc, ref DateTime buildTime)
		{
			ProcessModule processModule = null;
			this.SetLabelText(this.label1, "Waiting for librust to load...");
			while (processModule == null)
			{
				processModule = proc.Modules.OfType<ProcessModule>().FirstOrDefault((ProcessModule module) => module.ModuleName == "librust.dll");
				proc.Refresh();
				Thread.Sleep(250);
			}
			string path = Path.Combine(Path.GetTempPath(), "librust.tmp");
			byte[] bytes = this.memory.ReadBytes((uint)((int)processModule.BaseAddress), processModule.ModuleMemorySize);
			File.WriteAllBytes(path, bytes);
			this.SetLabelText(this.label1, "Running security check...");
			byte[] array = new byte[2048];
			Stream stream = null;
			try
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read);
				stream.Read(array, 0, 2048);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			int num = BitConverter.ToInt32(array, 60);
			int num2 = BitConverter.ToInt32(array, num + 8);
			buildTime = new DateTime(1970, 1, 1, 0, 0, 0);
			buildTime = buildTime.AddSeconds((double)num2);
			File.Delete(path);
			return buildTime <= this.highestBuild;
		}
		private string RndString(int length)
		{
			char[] array = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(array[new Random(Guid.NewGuid().GetHashCode()).Next(0, array.Length - 1)]);
			}
			return stringBuilder.ToString();
		}
		private int RndNumber(int min, int max)
		{
			return new Random(Guid.NewGuid().GetHashCode()).Next(min, max);
		}
		private void StartInjection()
		{
			while (true)
			{
				Process[] processesByName = Process.GetProcessesByName("rust");
				if (processesByName.Length > 0)
				{
					Process process = processesByName[0];
					this.SetLabelText(this.label1, "正在等待rust...");
					this.memory = new BlackMagic();
					this.memory.SetDebugPrivileges = false;
					this.memory.Open(process.Id);
					this.SetLabelText(this.label1, "正在注入!");
					MonoScriptLoader monoScriptLoader = new MonoScriptLoader(this.memory);
                    monoScriptLoader.LoadAssembly(DropHack(), "Rust_Jacked", "Entry", "Initialize");
					SystemSounds.Beep.Play();
					Thread.Sleep(1000);
					Process.GetCurrentProcess().Kill();
				}
				Thread.Sleep(500);
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FrmMain));
			this.label1 = new Label();
			base.SuspendLayout();
			this.label1.BackColor = Color.Transparent;
			this.label1.Font = new Font("Segoe UI", 7.75f);
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(12, 100);
			this.label1.Name = "label1";
			this.label1.Size = new Size(287, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Waiting for rust.exe";
			this.label1.TextAlign = ContentAlignment.MiddleRight;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.background;
			base.ClientSize = new Size(299, 120);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FrmMain";
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Load += new EventHandler(this.frmMain_Load);
			base.ResumeLayout(false);
		}
	}
}
