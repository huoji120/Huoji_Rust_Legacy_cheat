using Rust_Jacked.Hacks;
using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.GUIS
{
	internal class GUIMisc : MonoBehaviour
	{
		public static Rect startRect = new Rect(100f, 150f, 220f, 410f);
        private bool AUTO_WOOD_GATHER = false;
        private float LAST_ATTACK_TIME = 0f;
        private void OnGUI()
		{
			if (HackLocal.ShowMenu)
			{
				GUIMisc.startRect = GUI.Window(0, GUIMisc.startRect, new GUI.WindowFunction(this.DoMyWindow), "超级老枪 By huoji");
			}
            AutoWood();
            if (CVars.Misc.chuansong_wanjia)
            {
                GUI.color = Color.white;
                GUI.Box(new Rect(15f, 210f, 310f, 34f), "F6双油桶|F7动物园|F8平原|F9鸡棚|F10小工厂|F11二店|F12军营");
            }
        }
        public static void AllBlueprints()
        {
            Character component = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
            PlayerInventory playerInventory = component.GetComponent(typeof(PlayerInventory)) as PlayerInventory;
            if (playerInventory)
            {
                System.Collections.Generic.List<BlueprintDataBlock> boundBPs = playerInventory.GetBoundBPs();
                BlueprintDataBlock[] array = Facepunch.Bundling.LoadAll<BlueprintDataBlock>();
                for (int i = 0; i < array.Length; i++)
                {
                    BlueprintDataBlock blueprintDataBlock = array[i];
                    if (!boundBPs.Contains(blueprintDataBlock))
                    {
                        Rust.Notice.Inventory(" ", blueprintDataBlock.name);
                        boundBPs.Add(blueprintDataBlock);
                    }
                }
            }
        }
        public void AutoWood()
        {
            if (CVars.Misc.AutoWood)
            {
                WoodBlockerTemp.numWood = -1f;
                Character component = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                Inventory inventory = component.GetComponent(typeof(Inventory)) as Inventory;
                MeleeWeaponItem<MeleeWeaponDataBlock> meleeWeaponItem = inventory._activeItem as MeleeWeaponItem<MeleeWeaponDataBlock>;
                if (inventory._activeItem is MeleeWeaponItem<MeleeWeaponDataBlock> && Time.time - this.LAST_ATTACK_TIME > meleeWeaponItem.datablock.fireRate && this.AUTO_WOOD_GATHER)
                {
                    this.LAST_ATTACK_TIME = Time.time;
                    ItemRepresentation itemRepresentation = meleeWeaponItem.itemRepresentation;
                    IMeleeWeaponItem meleeWeaponItem2 = meleeWeaponItem.iface as IMeleeWeaponItem;
                    RaycastHit2 raycastHit;
                    bool flag = Physics2.Raycast2(component.eyesRay, out raycastHit, meleeWeaponItem.datablock.range, 406721553);
                    Vector3 point = raycastHit.point;
                    itemRepresentation.Action(3, uLink.RPCMode.Server);
                    uLink.BitStream bitStream = new uLink.BitStream(false);
                    if (flag)
                    {
                        IDMain idMain = raycastHit.idMain;
                        bitStream.WriteBoolean(true);
                        bitStream.Write<NetEntityID>(NetEntityID.Get(idMain), new object[0]);
                        bitStream.WriteVector3(point);
                        bitStream.WriteBoolean(false);
                        itemRepresentation.ActionStream(1, uLink.RPCMode.Server, bitStream);
                        HUDHitIndicator hUDHitIndicator;
                        if (Facepunch.Bundling.Load<HUDHitIndicator>("content/hud/HUDHitIndicator", out hUDHitIndicator))
                        {
                            HUDHitIndicator.CreateIndicator(point, true, hUDHitIndicator);
                        }
                    }
                    else
                    {
                        bitStream.WriteBoolean(false);
                        bitStream.WriteVector3(Vector3.zero);
                        bitStream.WriteBoolean(true);
                        itemRepresentation.ActionStream(1, uLink.RPCMode.Server, bitStream);
                    }
                }
            }
        }
        private void DoMyWindow(int windowID)
		{
			GUI.Label(new Rect(10f, 20f, 120f, 20f), string.Format("高跳 = ({0:0.#})", CVars.Misc.JumpModifer));
			CVars.Misc.JumpModifer = GUI.HorizontalSlider(new Rect(130f, 25f, 80f, 12f), CVars.Misc.JumpModifer, 1f, 30f);
			GUI.Label(new Rect(10f, 40f, 110f, 20f), string.Format("加速 = ({0:0.#})", CVars.Misc.SpeedModifer * 4f / 10f));
			CVars.Misc.SpeedModifer = GUI.HorizontalSlider(new Rect(130f, 45f, 200f, 12f), CVars.Misc.SpeedModifer, 10f, 100f);
			CVars.Misc.NoFallDamage = GUI.Toggle(new Rect(10f, 60f, 160f, 20f), CVars.Misc.NoFallDamage, "掉落无伤害");
			CVars.Misc.FlyHack = GUI.Toggle(new Rect(10f, 80f, 160f, 20f), CVars.Misc.FlyHack, "自由的飞");
			CVars.Misc.LightHack = GUI.Toggle(new Rect(10f, 100f, 160f, 20f), CVars.Misc.LightHack, "永远白天");
			CVars.Misc.NoRecoil = GUI.Toggle(new Rect(10f, 120f, 160f, 20f), CVars.Misc.NoRecoil, "无后坐力");
			CVars.Misc.NoReload = GUI.Toggle(new Rect(10f, 140f, 160f, 20f), CVars.Misc.NoReload, "自动换弹");
			CVars.Misc.ShowWatermark = GUI.Toggle(new Rect(10f, 160f, 160f, 20f), CVars.Misc.ShowWatermark, "总在工作台[失效]");
            CVars.Misc.blue = GUI.Toggle(new Rect(10f, 180f, 160f, 20f), CVars.Misc.blue, "辐射杀人[F键杀人]");
            CVars.Misc.wall = GUI.Toggle(new Rect(10f, 200f, 160f, 20f), CVars.Misc.wall, "穿墙模式");
            CVars.Misc.players = GUI.Toggle(new Rect(10f, 220f, 160f, 20f), CVars.Misc.players, "玩家接近报警");
            CVars.Misc.work = GUI.Toggle(new Rect(10f, 240f, 160f, 20f), CVars.Misc.work, "上帝模式[无视辐射]");
            CVars.Misc.zhafu = GUI.Toggle(new Rect(10f, 260f, 160f, 20f), CVars.Misc.zhafu, "炸服[后台乱码]");
            CVars.Misc.sb = GUI.Toggle(new Rect(10f, 280f, 160f, 20f), CVars.Misc.sb, "天使之吻[F键]");
            if (GUI.Button(new Rect(10f, 300f, 160f, 20f), "蓝图全解"))
            {
                AllBlueprints();
                Rust.Notice.Inventory("", "所有蓝图已经全解!");

            }
            CVars.Misc.AutoWood = GUI.Toggle(new Rect(10f, 320f, 160f, 20f), CVars.Misc.AutoWood, "无限木头[失效]");
            CVars.Misc.chuansong_wanjia = GUI.Toggle(new Rect(10f, 340f, 160f, 20f), CVars.Misc.chuansong_wanjia, "传送[配合断网器]");
            if (CVars.Misc.chuansong_wanjia)
            {
                GUI.Label(new Rect(10f, 360f, 160f, 20f), "X传|C飞天|Z瞬移");
            }
            CVars.Misc.door = GUI.Toggle(new Rect(10f, 380f, 160f, 20f), CVars.Misc.door, "多啦A梦任意门[失效]");
            GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}
	}
}
