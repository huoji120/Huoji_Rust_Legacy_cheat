using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	public class RaidESP : MonoBehaviour
	{
		private Shader shader;
		private Shader shaderStandart;
		private Shader shaderStandart1;
		private Shader shaderStandart2;
		private void Start()
		{
			this.shader = Shader.Find("GUI/Text Shader");
			this.shaderStandart = Shader.Find("Bumped Specular");
			this.shaderStandart1 = Shader.Find("Bumped Diffuse");
			this.shaderStandart2 = Shader.Find("Diffuse");
		}
		private void OnGUI()
		{
			try
			{
				if (Event.current.type == EventType.Repaint && HackLocal.IsIngame)
				{
					this.DrawRaidESP();
				}
			}
			catch
			{
			}
		}
		private void DrawRaidESP()
		{
			Character localCharacter = HackLocal.LocalCharacter;
			if (HackLocal.DoorObjects != null)
			{
				UnityEngine.Object[] doorObjects = HackLocal.DoorObjects;
				for (int i = 0; i < doorObjects.Length; i++)
				{
					UnityEngine.Object @object = doorObjects[i];
					if (!(@object == null))
					{
						BasicDoor basicDoor = (BasicDoor)@object;
						float num = Vector3.Distance(basicDoor.transform.position, localCharacter.transform.position);
						if (CVars.ESP.DrawRaid && num < 200f)
						{
							basicDoor.gameObject.renderer.material.shader = this.shader;
						}
						else
						{
							if (basicDoor.name.Contains("Metal"))
							{
								if (this.shaderStandart1 == null)
								{
									this.shaderStandart1 = basicDoor.gameObject.renderer.material.shader;
								}
								basicDoor.gameObject.renderer.material.shader = this.shaderStandart1;
							}
							else
							{
								if (basicDoor.name.Contains("Wooden"))
								{
									if (this.shaderStandart == null)
									{
										this.shaderStandart = basicDoor.gameObject.renderer.material.shader;
									}
									basicDoor.gameObject.renderer.material.shader = this.shaderStandart;
								}
								else
								{
									if (this.shaderStandart2 == null)
									{
										this.shaderStandart2 = basicDoor.gameObject.renderer.material.shader;
									}
									basicDoor.gameObject.renderer.material.shader = this.shaderStandart2;
								}
							}
						}
					}
				}
			}
			if (HackLocal.LootableObjects != null)
			{
				UnityEngine.Object[] lootableObjects = HackLocal.LootableObjects;
				for (int j = 0; j < lootableObjects.Length; j++)
				{
					UnityEngine.Object object2 = lootableObjects[j];
					if (!(object2 == null))
					{
						LootableObject lootableObject = (LootableObject)object2;
						float num2 = Vector3.Distance(lootableObject.transform.position, localCharacter.transform.position);
						if (lootableObject.name.Contains("WoodBox") || lootableObject.name.Contains("Stash"))
						{
							if (CVars.ESP.DrawRaid && num2 < 200f)
							{
								lootableObject.gameObject.renderer.material.shader = this.shader;
							}
							else
							{
								if (lootableObject.name.Contains("Stash"))
								{
									lootableObject.gameObject.renderer.material.shader = this.shaderStandart1;
								}
								else
								{
									lootableObject.gameObject.renderer.material.shader = this.shaderStandart;
								}
							}
						}
					}
				}
			}
		}
	}
}
