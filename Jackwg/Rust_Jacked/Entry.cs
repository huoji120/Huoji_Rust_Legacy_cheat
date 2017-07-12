using Rust_Jacked.GUIS;
using Rust_Jacked.Hacks;
using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked
{
	internal class Entry
	{
		private static GameObject menuObject;
		private static void Initialize()
		{
			CVars.Initialize();
			Entry.menuObject = new GameObject();
			Entry.menuObject.AddComponent<Bypass>();
			Entry.menuObject.AddComponent<HackLocal>();
			Entry.menuObject.AddComponent<GUIMisc>();
			Entry.menuObject.AddComponent<GUICrosshair>();
			Entry.menuObject.AddComponent<GUIAimbot>();
			Entry.menuObject.AddComponent<GUIEsp>();
			Entry.menuObject.AddComponent<MiscHacks>();
			Entry.menuObject.AddComponent<Aimbot>();
			Entry.menuObject.AddComponent<PlayerESP>();
			Entry.menuObject.AddComponent<LootESP>();
			Entry.menuObject.AddComponent<RaidESP>();
			Entry.menuObject.AddComponent<ResourceESP>();
			UnityEngine.Object.DontDestroyOnLoad(Entry.menuObject);
		}
	}
}
