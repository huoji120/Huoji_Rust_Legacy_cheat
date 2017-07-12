using Rust_Jacked.Rendering;
using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	public class LootESP : MonoBehaviour
	{
		private void OnGUI()
		{
			if (Event.current.type == EventType.Repaint && HackLocal.IsIngame)
			{
				try
				{
					this.DrawLoot();
				}
				catch
				{
				}
			}
		}
		private void DrawLoot()
		{
			if (!CVars.ESP.DrawLoot)
			{
				return;
			}
			UnityEngine.Object[] lootableObjects = HackLocal.LootableObjects;
			for (int i = 0; i < lootableObjects.Length; i++)
			{
				UnityEngine.Object @object = lootableObjects[i];
				if (!(@object == null))
				{
					LootableObject lootableObject = (LootableObject)@object;
					if (lootableObject.name.Contains("BoxLoot") || lootableObject.name.Contains("LootBox") || lootableObject.name.Contains("SupplyCrate") || lootableObject.name.Contains("LootSack"))
					{
						Color color = Color.white;
						if (lootableObject.name.Contains("LootSack"))
						{
							color = Color.cyan;
						}
						else
						{
							if (lootableObject.name.Contains("SupplyCrate"))
							{
								color = Color.magenta;
							}
						}
						Vector3 vector = Camera.main.WorldToScreenPoint(lootableObject.transform.position);
						if (vector.z > 0f)
						{
							string arg = lootableObject.name.Replace("(Clone)", "");
							vector.y = (float)Screen.height - (vector.y + 1f);
							Canvas.DrawString(new Vector2(vector.x, vector.y), color, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, string.Format("{0} [{1}]", arg, (int)vector.z));
						}
					}
				}
			}
		}
	}
}
