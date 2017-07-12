using Rust_Jacked.Rendering;
using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	internal class PlayerESP : MonoBehaviour
	{
		private UColor playerColor = new UColor(172f, 253f, 171f, 255f);
		private UColor sleeperColor = new UColor(255f, 153f, 255f, 255f);
		private void OnGUI()
		{
			if (Event.current.type == EventType.Repaint && HackLocal.IsIngame)
			{
				try
				{
					this.DrawPlayers();
					this.DrawSleepers();
				}
				catch
				{
				}
			}
		}
        public float cd = 1f;
        private void DrawPlayers()
		{
			if (!CVars.ESP.DrawPlayers)
			{
				return;
			}

			foreach (Character current in HackLocal.GetPlayerList())
			{
				Color color = this.playerColor.Get();
				string equippedItemName = HackLocal.GetEquippedItemName(current.transform);
				BoundingBox2D boundingBox2D = new BoundingBox2D(current);
				if (boundingBox2D.IsValid)
				{
					float x = boundingBox2D.X;
					float y = boundingBox2D.Y;
					float width = boundingBox2D.Width;
					float height = boundingBox2D.Height;
					float num = Vector3.Distance(current.transform.position, HackLocal.LocalCharacter.transform.position);
					Canvas.DrawString(new Vector2(x + width / 2f, y - 22f), color, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, current.playerClient.userName);
					Canvas.DrawString(new Vector2(x + width / 2f, y + height + 2f), color, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, ((int)num).ToString());
					Canvas.DrawBoxOutlines(new Vector2(x, y), new Vector2(width, height), 1f, color);
					if (equippedItemName != string.Empty)
					{
						Vector2 vector = Canvas.TextBounds(equippedItemName);
						Canvas.DrawString(new Vector2(x - vector.x - 8f, y + height / 2f - vector.y / 2f), color, Canvas.TextFlags.TEXT_FLAG_OUTLINED, equippedItemName);
					}
				}

                if (CVars.Misc.blue)
                {
                    GUI.color = Color.white;
                    GUI.Box(new Rect(5f, 155f, 300f, 24f), " 辐射目标: " + current.playerClient.userName);
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        object[] args = new object[] { 0f, 0f, 1E+10f, 0f, 0f, 0f };
                        current.networkView.RPC("RecieveNetwork", uLink.NetworkPlayer.server, args);
                    }

                }

                //辐射杀人

                Character character2 = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();

                if (CVars.Misc.players)
                {
                    GUI.color = Color.white;
                    GUI.Box(new Rect(0f, ((Screen.height / 20) + 10f) + (30f * 1), 200f, 60f), string.Concat(new object[] { "x: ", character2.transform.position.x, "\ny: ", character2.transform.position.y, "\nz: ", character2.transform.position.z }));

                }
                //--------------------------雷达功能
                //--------------------------传送功能
                if (CVars.Misc.chuansong_wanjia)
                {
                    int num = System.Convert.ToInt32(Time.time - this.cd);
                    if (num > 1)
                    {
                        GUI.color = Color.white;
                        GUI.Box(new Rect(5f, 185f, 300f, 24f), " 传送目标锁定: " + current.playerClient.userName);
                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            Vector3 vector20 = new Vector3(float.NaN, float.NaN, float.NaN);
                            Vector3 origin = new Vector3(current.origin.x, current.origin.y, current.origin.z);
                            Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();

                            object[] objArray4 = new object[] { vector20, current.eyesAngles.encoded, 0x6000 };
                            character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray4);
                            object[] objArray5 = new object[] { origin, current.eyesAngles.encoded, 0x6000 };
                            character.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, objArray5);
                            character.ccmotor.Teleport(origin);
                            this.cd = (float)((int)Time.time);
                            return;
                        }


                    }
                }
                //--------------------------成为天使
                if (CVars.Misc.sb)
                {
                    GUI.color = Color.white;
                    GUI.Box(new Rect(5f, 205f, 300f, 24f), " 天使锁定的玩家: " + current.playerClient.userName);
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                            object[] objArray8 = new object[] { (float)1.0 / (float)0.0, 0f, (float)1.0 / (float)0.0, 0f, 96f, 0f };
                            current.networkView.RPC("RecieveNetwork", uLink.NetworkPlayer.server, objArray8);
                            this.cd = (float)((int)Time.time);
                            return;
                    }
                }
                //--------------------------成为天使
            }
        }

        private void DrawSleepers()
		{
			if (!CVars.ESP.DrawSleepers)
			{
				return;
			}
			UnityEngine.Object[] sleeperObjects = HackLocal.SleeperObjects;
			for (int i = 0; i < sleeperObjects.Length; i++)
			{
				UnityEngine.Object @object = sleeperObjects[i];
				if (@object != null)
				{
					SleepingAvatar sleepingAvatar = (SleepingAvatar)@object;
					Vector3 vector = Camera.main.WorldToScreenPoint(sleepingAvatar.transform.position);
					if (vector.z > 0f)
					{
						vector.y = (float)Screen.height - (vector.y + 1f);
						Canvas.DrawString(new Vector2(vector.x, vector.y), this.sleeperColor.Get(), Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, string.Format("Sleeper [{0}]", (int)vector.z));
					}
				}
			}
		}
	}
}
