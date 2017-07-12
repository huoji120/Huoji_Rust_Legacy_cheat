using Magic;
using System;
using System.Diagnostics;
using System.Text;
namespace Jacked_Loader
{
	public class MonoScriptLoader
	{
		private BlackMagic memory;
		public MonoScriptLoader(BlackMagic _memory)
		{
			this.memory = _memory;
		}
		private uint FindFuncAdress(string moduleName, string functionName)
		{
			uint num = 0u;
			foreach (ProcessModule processModule in this.memory.Modules)
			{
				if (processModule.ModuleName == moduleName)
				{
					num = (uint)((int)processModule.BaseAddress);
					break;
				}
			}
			if (num == 0u)
			{
				throw new Exception(moduleName + "  not found");
			}
			uint num2 = this.memory.ReadUInt(num + 60u);
			num2 = this.memory.ReadUInt(num + num2 + 120u);
			int num3 = this.memory.ReadInt(num + num2 + 24u);
			uint num4 = this.memory.ReadUInt(num + num2 + 32u);
			int num5 = -1;
			for (int i = 0; i < num3; i++)
			{
				uint num6 = this.memory.ReadUInt(num + num4 + (uint)(i * 4));
				string a = this.memory.ReadASCIIString(num + num6, 64);
				if (a == functionName)
				{
					num5 = i;
					break;
				}
			}
			if (num5 == -1)
			{
				throw new Exception(functionName + "  not found in module");
			}
			uint num7 = this.memory.ReadUInt(num + num2 + 36u) + num;
			int num8 = (int)this.memory.ReadShort(num7 + (uint)(num5 * 2));
			uint num9 = this.memory.ReadUInt(num + num2 + 28u) + num;
			return this.memory.ReadUInt(num9 + (uint)(num8 * 4)) + num;
		}
		private uint AllocRemoteString(string value)
		{
			uint num = this.memory.AllocateMemory(Encoding.UTF8.GetBytes(value).Length + 1);
			this.memory.WriteBytes(num, Encoding.UTF8.GetBytes(value));
			return num;
		}
		private uint GetRelativeAddress(uint address, uint target)
		{
			return address - target - 4u;
		}
		public void LoadAssembly(string path, string entryNamespace, string entryClass, string entryFunction)
		{
			uint num = this.AllocRemoteString(path);
			uint num2 = this.AllocRemoteString(entryNamespace);
			uint num3 = this.AllocRemoteString(entryClass);
			uint num4 = this.AllocRemoteString(entryFunction);
			byte[] array = new byte[]
			{
				232,
				0,
				0,
				0,
				0,
				80,
				232,
				0,
				0,
				0,
				0,
				184,
				0,
				0,
				0,
				0,
				106,
				0,
				80,
				232,
				0,
				0,
				0,
				0,
				80,
				232,
				0,
				0,
				0,
				0,
				186,
				0,
				0,
				0,
				0,
				82,
				185,
				0,
				0,
				0,
				0,
				81,
				80,
				232,
				0,
				0,
				0,
				0,
				106,
				0,
				185,
				0,
				0,
				0,
				0,
				81,
				80,
				232,
				0,
				0,
				0,
				0,
				106,
				0,
				106,
				0,
				106,
				0,
				80,
				232,
				0,
				0,
				0,
				0,
				131,
				196,
				56,
				195
			};
			uint num5 = this.memory.AllocateMemory(array.Length);
			this.memory.WriteBytes(num5, array);
			this.memory.WriteUInt(num5 + 1u, this.GetRelativeAddress(this.FindFuncAdress("mono.dll", "mono_get_root_domain"), num5 + 1u));
			this.memory.WriteUInt(num5 + 7u, this.GetRelativeAddress(this.FindFuncAdress("mono.dll", "mono_thread_attach"), num5 + 7u));
			this.memory.WriteUInt(num5 + 12u, num);
			this.memory.WriteUInt(num5 + 20u, this.GetRelativeAddress(this.FindFuncAdress("mono.dll", "mono_assembly_open"), num5 + 20u));
			this.memory.WriteUInt(num5 + 26u, this.GetRelativeAddress(this.FindFuncAdress("mono.dll", "mono_assembly_get_image"), num5 + 26u));
			this.memory.WriteUInt(num5 + 31u, num3);
			this.memory.WriteUInt(num5 + 37u, num2);
			this.memory.WriteUInt(num5 + 44u, this.GetRelativeAddress(this.FindFuncAdress("mono.dll", "mono_class_from_name"), num5 + 44u));
			this.memory.WriteUInt(num5 + 51u, num4);
			this.memory.WriteUInt(num5 + 58u, this.GetRelativeAddress(this.FindFuncAdress("mono.dll", "mono_class_get_method_from_name"), num5 + 58u));
			this.memory.WriteUInt(num5 + 70u, this.GetRelativeAddress(this.FindFuncAdress("mono.dll", "mono_runtime_invoke"), num5 + 70u));
			IntPtr hObject = this.memory.CreateRemoteThread(num5, 0u);
			bool flag = false;
			if (SThread.WaitForSingleObject(hObject, 3000u) != 0u)
			{
				flag = true;
			}
			this.memory.FreeMemory(num);
			this.memory.FreeMemory(num3);
			this.memory.FreeMemory(num2);
			this.memory.FreeMemory(num4);
			this.memory.FreeMemory(num5);
			if (flag)
			{
				throw new Exception("Timeout in mono loader");
			}
		}
	}
}
