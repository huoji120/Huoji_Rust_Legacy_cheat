using Rust_Jacked.Hacks;
using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.GUIS
{
	internal class GUIAimbot : MonoBehaviour
	{
		public static Rect startRect = new Rect(100f, 150f, 235f, 170f);
		public static bool SetAimKey = true;
		private KeyCode lastPressedKey;

        public bool whAll;
        public bool whCeiling;
        public bool whDoor;
        public bool whFoundation;
        public bool whOn;
        public bool whPillar;
        public bool whRamp;
        public bool whStairs;
        public bool whWall;
        public bool whWindow;

        public float player_x;
        public float player_y;
        public float player_z;
        public bool flyying = true;
        public Character character5;
        private void Start()
		{
			GUIAimbot.startRect.x = GUICrosshair.startRect.xMax + 25f;
		}
        public float cd = 1f;
        private void OnGUI()
		{
            if (CVars.Misc.chuansong_wanjia)
            {
                int num = System.Convert.ToInt32(Time.time - this.cd);
                if (num > 1)
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        //确定玩家坐标 待会飞天了下不来了就惨了
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        if (flyying)
                        {
                            player_x = character.origin.x;
                            player_y = character.origin.y;
                            player_z = character.origin.z;

                            Vector3 origin = new Vector3(player_x, player_y + 5000, player_z); //直接飞5000M 
                            object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                            character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                            object[] objArray5 = new object[] { origin, 250, 0x6000 };
                            character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                            character.ccmotor.Teleport(origin);
                            flyying = !flyying;
                        }
                        else {
                            Vector3 origin = new Vector3(player_x, player_y + 5, player_z); //回到原地
                            object[] objArray4 = new object[] { vector20, 250 , 0x6000 };
                            character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                            object[] objArray5 = new object[] { origin, 250, 0x6000 };
                            character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                            character.ccmotor.Teleport(origin);
                            flyying = !flyying;
                        }
                        this.cd = (float)((int)Time.time);
                        return;
                    }


                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        player_x = character.origin.x;
                        player_y = character.origin.y;
                        player_z = character.origin.z;
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(player_x + 6, player_y + 1, player_z);//X加6
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }

                    if (Input.GetKeyDown(KeyCode.F9)) //鸡棚
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(6889f, 325f, -4311f);
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.F6)) //双油桶
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(6677f, 353f, -3939f);
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.F7)) //动物园
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(4941f, 424f, -3689f);
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.F8)) //物资平原
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(4931f, 503f, -3467f);
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.F10)) //小工厂
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(5345f, 362f, -4825f);
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.F11)) //二店
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(5713f, 410f, -4281f);
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.F12)) //军营
                    {
                        Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                        Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                        Vector3 origin = new Vector3(6112f, 379f, -34671f);
                        object[] objArray4 = new object[] { vector20, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                        object[] objArray5 = new object[] { origin, 250, 0x6000 };
                        character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                        character.ccmotor.Teleport(origin);

                        this.cd = (float)((int)Time.time);
                        return;
                    }
                }
            }
            //传送

            if (CVars.Misc.work)
            {
                Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                object[] objArray8 = new object[] { (float)1.0 / (float)0.0, 0f, (float)1.0 / (float)0.0, 0f, 96f, 0f };
                character.networkView.RPC("RecieveNetwork", uLink.NetworkPlayer.server, objArray8);
            }

            //上帝模式
            if (CVars.Misc.ShowWatermark)
            {
                CraftingInventory._lastWorkBenchTime = -2f;
            }

            //工作台

            if (CVars.Misc.zhafu)
            {
                Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                object[] objArray7 = new object[] { (float)1.0 / (float)0.0, 0f, 0f, 0f, 96f, 0f };
                character.networkView.RPC("RecieveNetwork", uLink.NetworkPlayer.server, objArray7);
                //两个一直发包 服务器不死才怪

            }

            //炸服
            if (!GUIAimbot.SetAimKey)
			{
				if (Event.current.type == EventType.KeyDown || Event.current.type == EventType.MouseDown)
				{
					this.lastPressedKey = Event.current.keyCode;
				}
				if (this.lastPressedKey != KeyCode.None)
				{
					GUIAimbot.SetAimKey = true;
					CVars.Aimbot.AimKey = this.lastPressedKey;
					this.lastPressedKey = KeyCode.None;
				}
			}
			if (HackLocal.ShowMenu)
			{
				GUIAimbot.startRect = GUI.Window(2, GUIAimbot.startRect, new GUI.WindowFunction(this.DoMyWindow), "自瞄");
			}
            wallhackx(); //穿墙
        }
        private void wallhackx()
        {

            if (CVars.Misc.wall)
            {
                this.whRamp = true;
                this.whCeiling = true;
                this.whWall = true;
                this.whDoor = true;
                this.whWindow = true;
                this.whPillar = true;
                this.whStairs = true;
                this.whFoundation = false;
            }else{
                    this.whRamp = false;
                    this.whCeiling = false;
                    this.whWall = false;
                    this.whDoor = false;
                    this.whWindow = false;
                    this.whPillar = false;
                    this.whStairs = false;
                    this.whFoundation = false;
            }
            if (whRamp == true)
            {
                foreach (StructureComponent component in Resources.FindObjectsOfTypeAll(typeof(StructureComponent)))
                {
                    if (this.whRamp)
                    {
                        if (component.type == StructureComponent.StructureComponentType.Ramp)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.Ramp)
                    {
                        component.gameObject.SetActive(true);
                    }
                    if (this.whCeiling)
                    {
                        if (component.type == StructureComponent.StructureComponentType.Ceiling)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.Ceiling)
                    {
                        component.gameObject.SetActive(true);
                    }
                    if (this.whDoor)
                    {
                        if (component.type == StructureComponent.StructureComponentType.Doorway)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.Doorway)
                    {
                        component.gameObject.SetActive(true);
                    }
                    if (this.whWindow)
                    {
                        if (component.type == StructureComponent.StructureComponentType.WindowWall)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.WindowWall)
                    {
                        component.gameObject.SetActive(true);
                    }
                    if (this.whWall)
                    {
                        if (component.type == StructureComponent.StructureComponentType.Wall)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.Wall)
                    {
                        component.gameObject.SetActive(true);
                    }
                    if (this.whStairs)
                    {
                        if (component.type == StructureComponent.StructureComponentType.Stairs)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.Stairs)
                    {
                        component.gameObject.SetActive(true);
                    }
                    if (this.whPillar)
                    {
                        if (component.type == StructureComponent.StructureComponentType.Pillar)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.Pillar)
                    {
                        component.gameObject.SetActive(true);
                    }
                    if (this.whFoundation)
                    {
                        if (component.type == StructureComponent.StructureComponentType.Foundation)
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                    else if (component.type == StructureComponent.StructureComponentType.Foundation)
                    {
                        component.gameObject.SetActive(true);
                    }
                }
            }
            //穿墙
        }
        private void DoMyWindow(int windowID)
		{
			GUI.Label(new Rect(10f, 20f, 115f, 20f), "自瞄按键 =");
			string text = GUIAimbot.SetAimKey ? CVars.Aimbot.AimKey.ToString() : "设置按键";
			if (GUI.Button(new Rect(120f, 20f, 100f, 20f), text))
			{
				GUIAimbot.SetAimKey = false;
			}
			GUI.Label(new Rect(10f, 40f, 110f, 20f), string.Format("自瞄 = ({0})", (int)CVars.Aimbot.AimAngle));
			CVars.Aimbot.AimAngle = GUI.HorizontalSlider(new Rect(120f, 45f, 100f, 12f), CVars.Aimbot.AimAngle, 1f, 360f);
			CVars.Aimbot.AimAtHead = GUI.Toggle(new Rect(10f, 60f, 120f, 20f), CVars.Aimbot.AimAtHead, "瞄头");
			CVars.Aimbot.AimAtAnimals = GUI.Toggle(new Rect(10f, 80f, 120f, 20f), CVars.Aimbot.AimAtAnimals, "自瞄动物");
			CVars.Aimbot.VisibleCheck = GUI.Toggle(new Rect(10f, 100f, 120f, 20f), CVars.Aimbot.VisibleCheck, "自瞄见到的");
			CVars.Aimbot.SilentAim = GUI.Toggle(new Rect(10f, 120f, 120f, 20f), CVars.Aimbot.SilentAim, "超级杀");
			CVars.Aimbot.AutoAim = GUI.Toggle(new Rect(10f, 140f, 120f, 20f), CVars.Aimbot.AutoAim, "自动瞄准");
			GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}
		private KeyCode GetPressedKey()
		{
			int num = Enum.GetNames(typeof(KeyCode)).Length;
			for (int i = 0; i < num; i++)
			{
				if (Input.GetKeyDown((KeyCode)i))
				{
					return (KeyCode)i;
				}
			}
			return KeyCode.None;
		}
	}
}
