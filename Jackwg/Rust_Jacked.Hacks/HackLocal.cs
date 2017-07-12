using Facepunch.Cursor;
using Rust_Jacked.Rendering;
using Rust_Jacked.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	internal class HackLocal : MonoBehaviour
	{
		private UnlockCursorNode cursor;
		public static UnityEngine.Object[] PlayerObjects
		{
			get;
			set;
		}
		public static UnityEngine.Object[] CharacterObjects
		{
			get;
			set;
		}
		public static UnityEngine.Object[] LootableObjects
		{
			get;
			set;
		}
		public static UnityEngine.Object[] DoorObjects
		{
			get;
			set;
		}
		public static UnityEngine.Object[] ResourceObjects
		{
			get;
			set;
		}
		public static UnityEngine.Object[] SleeperObjects
		{
			get;
			set;
		}
		public static bool IsIngame
		{
			get;
			set;
		}
		public static bool ShowMenu
		{
			get;
			set;
		}
		public static Character LocalCharacter
		{
			get;
			set;
		}
		public static PlayerClient LocalPlayerClient
		{
			get;
			set;
		}
		public static HumanController LocalController
		{
			get;
			set;
		}
		private void Start()
		{
			base.StartCoroutine(this.UpdateObjects());
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Insert) && Input.GetKeyDown(KeyCode.F8))
			{
				HackLocal.ShowMenu = !HackLocal.ShowMenu;
			}
			if (this.cursor == null)
			{
				this.cursor = LockCursorManager.CreateCursorUnlockNode(false, "Death Screen");
			}
			this.cursor.On = HackLocal.ShowMenu;
			Canvas.UpdateFPS();
		}
		private void OnGUI()
		{
			if (Event.current.type == EventType.Repaint)
			{
				//Canvas.DrawWatermark();
				//Canvas.DrawFPS();
				Canvas.DrawCrosshair();
			}
		}
		private IEnumerator UpdateObjects()
		{
			while (true)
			{
				yield return new WaitForSeconds(0.5f);
				try
				{
					HackLocal.IsIngame = false;
					HackLocal.LocalPlayerClient = PlayerClient.GetLocalPlayer();
					if (HackLocal.LocalPlayerClient == null)
					{
						continue;
					}
					Controllable controllable = HackLocal.LocalPlayerClient.controllable;
					if (controllable == null)
					{
						continue;
					}
					HackLocal.LocalCharacter = controllable.character;
					if (HackLocal.LocalCharacter == null)
					{
						continue;
					}
					HackLocal.LocalController = (HackLocal.LocalCharacter.controller as HumanController);
					if (HackLocal.LocalCharacter.gameObject == null || HackLocal.LocalController == null)
					{
						continue;
					}
					HackLocal.IsIngame = true;
					if (CVars.ESP.DrawPlayers)
					{
						HackLocal.PlayerObjects = UnityEngine.Object.FindObjectsOfType<Player>();
					}
					if (CVars.ESP.DrawRaid || CVars.ESP.DrawLoot)
					{
						HackLocal.LootableObjects = UnityEngine.Object.FindObjectsOfType<LootableObject>();
					}
					if (CVars.ESP.DrawRaid)
					{
						HackLocal.DoorObjects = UnityEngine.Object.FindObjectsOfType<BasicDoor>();
					}
					if (CVars.ESP.DrawResources)
					{
						HackLocal.ResourceObjects = UnityEngine.Object.FindObjectsOfType<ResourceObject>();
					}
					if (CVars.ESP.DrawAnimals || CVars.Aimbot.AimAtAnimals)
					{
						HackLocal.CharacterObjects = UnityEngine.Object.FindObjectsOfType<Character>();
					}
					if (CVars.ESP.DrawSleepers)
					{
						HackLocal.SleeperObjects = UnityEngine.Object.FindObjectsOfType<SleepingAvatar>();
					}
					continue;
				}
				catch
				{
					continue;
				}
				yield break;
			}
		}
		public static List<Character> GetPlayerList()
		{
			List<Character> list = new List<Character>();
			UnityEngine.Object[] playerObjects = HackLocal.PlayerObjects;
			for (int i = 0; i < playerObjects.Length; i++)
			{
				UnityEngine.Object @object = playerObjects[i];
				if (!(@object == null))
				{
					Player player = (Player)@object;
					if (!(player.gameObject == HackLocal.LocalCharacter.gameObject) && !(player.playerClient == null) && player.alive && !player.dead)
					{
						list.Add(player.character);
					}
				}
			}
			return list;
		}
		public static List<Character> GetAnimalList()
		{
			List<Character> list = new List<Character>();
			UnityEngine.Object[] characterObjects = HackLocal.CharacterObjects;
			for (int i = 0; i < characterObjects.Length; i++)
			{
				UnityEngine.Object @object = characterObjects[i];
				if (!(@object == null))
				{
					Character character = (Character)@object;
					if (!(character == null))
					{
						PlayerClient playerClient = character.playerClient;
						if (!(playerClient != null) && character.alive && !character.dead && !character.name.Contains("Ragdoll"))
						{
							list.Add(character);
						}
					}
				}
			}
			return list;
		}
		public static void LogToConsole(string message)
		{
			ConsoleWindow consoleWindow = UnityEngine.Object.FindObjectOfType<ConsoleWindow>();
			if (consoleWindow == null)
			{
				return;
			}
			consoleWindow.AddText(message, false);
		}
		public static Transform GetHeadBone(Character character)
		{
			Transform[] componentsInChildren = character.GetComponentsInChildren<Transform>(false);
			Transform[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Transform transform = array[i];
				if (transform.gameObject.name.Contains("_Head1") || transform.gameObject.name == "Head")
				{
					return transform;
				}
			}
			return null;
		}
		public static Transform GetBodyBone(Character character)
		{
			Transform[] componentsInChildren = character.GetComponentsInChildren<Transform>(false);
			Transform[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Transform transform = array[i];
				if (transform.gameObject.name.Contains("Pelvis"))
				{
					return transform;
				}
			}
			return null;
		}
		public static Transform GetEyeBone(Character character)
		{
			Transform[] componentsInChildren = character.GetComponentsInChildren<Transform>(false);
			Transform[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Transform transform = array[i];
				if (transform.gameObject.name == "Eyes")
				{
					return transform;
				}
			}
			return null;
		}
		public static InventoryItem GetCurrentEquippedItem(Character character)
		{
			InventoryHolder component = character.GetComponent<InventoryHolder>();
			if (component == null)
			{
				return null;
			}
			if (component.itemRepresentation != null)
			{
				return (InventoryItem)component.inputItem;
			}
			return null;
		}
		public static InventoryItem GetCurrentEquippedItem(Controller controller)
		{
			Inventory component = controller.GetComponent<Inventory>();
			if (component && component.activeItem != null && component.activeItem.datablock != null)
			{
				return (InventoryItem)component.activeItem;
			}
			return null;
		}
		public static string GetEquippedItemName(Transform parent)
		{
			string empty = string.Empty;
			Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
			Transform[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Transform transform = array[i];
				ItemRepresentation component = transform.gameObject.GetComponent<ItemRepresentation>();
				if (!(component == null))
				{
					return component.datablock.name;
				}
			}
			return empty;
		}
	}
}
