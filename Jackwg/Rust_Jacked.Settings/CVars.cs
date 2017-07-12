using System;
using UnityEngine;
namespace Rust_Jacked.Settings
{
	internal class CVars
	{
		internal class Aimbot
		{
			public static KeyCode AimKey;
			public static float AimAngle;
			public static bool AimAtHead;
			public static bool AimAtAnimals;
			public static bool VisibleCheck;
			public static bool SilentAim;
			public static bool AutoAim;
			public static bool AutoShoot;
		}
		internal class Crosshair
		{
			public static bool Enable;
			public static int Color;
			public static int Opacity;
			public static int Size;
			public static int Style;
		}
		internal class ESP
		{
			public static bool DrawPlayers;
			public static bool DrawLoot;
			public static bool DrawRaid;
			public static bool DrawSleepers;
			public static bool DrawResources;
			public static bool DrawAnimals;
		}
		internal class Misc
		{
			public static float JumpModifer;
			public static float SpeedModifer;
			public static bool NoFallDamage;
			public static bool FlyHack;
			public static bool LightHack;
			public static bool NoRecoil;
			public static bool NoReload;
			public static bool ShowWatermark;
            public static bool work;
            public static bool blue;
            public static bool wall;
            public static bool players;
            public static bool zhafu;
            public static bool AutoWood;
            public static bool sb;
            public static bool chuansong_wanjia;
            public static bool door;
        }
		public static void Initialize()
		{
			CVars.Aimbot.AimKey = KeyCode.Q;
			CVars.Aimbot.AimAngle = 255f;
			CVars.Aimbot.AimAtHead = false;
			CVars.Aimbot.AimAtAnimals = false;
			CVars.Aimbot.VisibleCheck = false;
			CVars.Aimbot.SilentAim = false;
			CVars.Aimbot.AutoAim = false;
			CVars.Aimbot.AutoShoot = false;
			CVars.Crosshair.Enable = true;
			CVars.Crosshair.Style = 0;
			CVars.Crosshair.Color = 0;
			CVars.Crosshair.Opacity = 255;
			CVars.Crosshair.Size = 10;
			CVars.ESP.DrawPlayers = true;
			CVars.ESP.DrawLoot = false;
			CVars.ESP.DrawRaid = false;
			CVars.ESP.DrawAnimals = false;
			CVars.ESP.DrawSleepers = false;
			CVars.ESP.DrawResources = false;
			CVars.Misc.JumpModifer = 1f;
			CVars.Misc.SpeedModifer = 10f;
			CVars.Misc.NoFallDamage = true;
			CVars.Misc.FlyHack = false;
			CVars.Misc.LightHack = false;
			CVars.Misc.NoRecoil = true;
			CVars.Misc.NoReload = true;
			CVars.Misc.ShowWatermark = false;
            CVars.Misc.work = false;
            CVars.Misc.blue = false;
            CVars.Misc.wall = false;
            CVars.Misc.players = true;
            CVars.Misc.zhafu = false;
            CVars.Misc.AutoWood = false;
            CVars.Misc.sb = false;
            CVars.Misc.chuansong_wanjia = false;
            CVars.Misc.door = false;
        }
	}
}
