using Rust_Jacked.GUIS;
using Rust_Jacked.Rendering;
using Rust_Jacked.Settings;
using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;
using Facepunch;
namespace Rust_Jacked.Hacks
{
	internal class Aimbot : UnityEngine.MonoBehaviour
	{
		private Vector2 heliosBoxPos = Vector2.zero;
		private void OnGUI()
		{
			if (this.heliosBoxPos != Vector2.zero)
			{
				Canvas.HeliosBox(this.heliosBoxPos.x, this.heliosBoxPos.y);
			}
		}
		private void Update()
		{
			if (!HackLocal.IsIngame)
			{
				return;
			}
			this.Update_AimBot();
		}
		private float GetRotationFov(Character localCharacte, ref Vector3 startPos, ref Vector3 endPos)
		{
			Angle2 normalized = Angle2.LookDirection((endPos - startPos).normalized).normalized;
			normalized.pitch -= localCharacte.eyesAngles.pitch;
			normalized.yaw -= localCharacte.eyesAngles.yaw;
			return (float)Math.Sqrt((double)(normalized.pitch * normalized.pitch + normalized.yaw * normalized.yaw));
		}
		public void GetAimPosition(Character localCharacter, Character targetCharacter, ref Vector3 startPosition, ref Vector3 endPosition)
		{
			startPosition = localCharacter.transform.position;
			endPosition = targetCharacter.transform.position;
			Transform eyeBone = HackLocal.GetEyeBone(localCharacter);
			Transform transform = CVars.Aimbot.AimAtHead ? HackLocal.GetHeadBone(targetCharacter) : HackLocal.GetBodyBone(targetCharacter);
			startPosition.y += 1f;
			if (eyeBone != null)
			{
				startPosition = eyeBone.position;
			}
			endPosition.y += 1f;
			if (transform != null)
			{
				endPosition = transform.position;
			}
		}
		public void AutoAimAtPlayer(Character localCharacter, Character targetCharacter)
		{
			Vector3 b = new Vector3(0f, 0f, 0f);
			Vector3 a = new Vector3(0f, 0f, 0f);
			this.GetAimPosition(localCharacter, targetCharacter, ref b, ref a);
			Vector3 v = a - b;
			v.Normalize();
			Angle2 normalized = Angle2.LookDirection(v).normalized;
			localCharacter.eyesAngles = normalized;
		}
        
		private bool IsTargetVisible(Character localCharacter, Character targetCharacter)
		{
			Vector3 start = new Vector3(0f, 0f, 0f);
			Vector3 end = new Vector3(0f, 0f, 0f);
			this.GetAimPosition(localCharacter, targetCharacter, ref start, ref end);
			RaycastHit raycastHit;
			return end.y <= 1500f && (!Physics.Linecast(start, end, out raycastHit, 525313) || raycastHit.transform.gameObject == targetCharacter.gameObject);
		}
		private bool IsTargetInFOV(Character localCharacter, Character targetCharacter)
		{
			Vector3 vector = new Vector3(0f, 0f, 0f);
			Vector3 vector2 = new Vector3(0f, 0f, 0f);
			this.GetAimPosition(localCharacter, targetCharacter, ref vector, ref vector2);
			return CVars.Aimbot.AimAngle >= 360f || this.GetRotationFov(localCharacter, ref vector, ref vector2) <= CVars.Aimbot.AimAngle;
		}
		private bool ValidatePlayerClient_ForTarget(Character targetCharacter)
		{
			Character localCharacter = HackLocal.LocalCharacter;
			return this.IsTargetInFOV(localCharacter, targetCharacter) && (!CVars.Aimbot.VisibleCheck || this.IsTargetVisible(localCharacter, targetCharacter));
		}
		private Character GetClosestToCrosshair()
		{
			Character arg_05_0 = HackLocal.LocalCharacter;
			Character result = null;
			float num = 99999f;
			float num2 = (float)(Screen.width / 2);
			float num3 = (float)(Screen.height / 2);
			List<Character> playerList = HackLocal.GetPlayerList();
			if (CVars.Aimbot.AimAtAnimals)
			{
				foreach (Character current in HackLocal.GetAnimalList())
				{
					playerList.Add(current);
				}
			}
			foreach (Character current2 in playerList)
			{
				if (this.ValidatePlayerClient_ForTarget(current2))
				{
					Vector3 vector = Camera.main.WorldToScreenPoint(current2.transform.position);
					if (vector.z >= 0f)
					{
						vector.y = (float)Screen.height - (vector.y + 1f);
						float num4;
						if (vector.x > num2)
						{
							num4 = vector.x - num2;
						}
						else
						{
							num4 = num2 - vector.x;
						}
						float num5;
						if (vector.y > num3)
						{
							num5 = vector.y - num3;
						}
						else
						{
							num5 = num3 - vector.y;
						}
						float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
						if (num6 < num)
						{
							result = current2;
							num = num6;
						}
					}
				}
			}
			return result;
		}
		private bool SilentAim(Character localCharacter, Character targetCharacter)
		{
			HumanController component = localCharacter.GetComponent<HumanController>();
			InventoryItem currentEquippedItem = HackLocal.GetCurrentEquippedItem(component);
			if (currentEquippedItem == null)
			{
				return false;
			}
			uLink.BitStream bitStream = new uLink.BitStream(false);
			if (currentEquippedItem is BulletWeaponItem<BulletWeaponDataBlock>)
			{
				BulletWeaponItem<BulletWeaponDataBlock> bulletWeaponItem = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
				bitStream.WriteByte(9);
				bitStream.Write<NetEntityID>(NetEntityID.Get(targetCharacter), new object[0]);
				bitStream.WriteVector3(targetCharacter.transform.position);
				bulletWeaponItem.itemRepresentation.ActionStream(1, uLink.RPCMode.Server, bitStream);
			}
			else
			{
				if (currentEquippedItem is BowWeaponItem<BowWeaponDataBlock>)
				{
					BowWeaponItem<BowWeaponDataBlock> bowWeaponItem = currentEquippedItem as BowWeaponItem<BowWeaponDataBlock>;
					bitStream.Write<NetEntityID>(NetEntityID.Get(targetCharacter), new object[0]);
					bitStream.Write<Vector3>(targetCharacter.transform.position, new object[0]);
					bowWeaponItem.itemRepresentation.ActionStream(2, uLink.RPCMode.Server, bitStream);
				}
				else
				{
					if (!(currentEquippedItem is BulletWeaponItem<ShotgunDataBlock>))
					{
						return false;
					}
					BulletWeaponItem<ShotgunDataBlock> bulletWeaponItem2 = currentEquippedItem as BulletWeaponItem<ShotgunDataBlock>;
					for (int i = 0; i < bulletWeaponItem2.datablock.numPellets; i++)
					{
						bitStream.WriteByte(9);
						bitStream.Write<NetEntityID>(NetEntityID.Get(targetCharacter), new object[0]);
						bitStream.WriteVector3(targetCharacter.transform.position);
					}
					bulletWeaponItem2.itemRepresentation.ActionStream(1, uLink.RPCMode.Server, bitStream);
				}
			}
			return true;
		}
		private void Update_AimBot()
		{
			this.heliosBoxPos = Vector2.zero;
			if (ChatUI.IsVisible() || !GUIAimbot.SetAimKey)
			{
				return;
			}
			if (!Input.GetKey(CVars.Aimbot.AimKey) && !CVars.Aimbot.AutoAim)
			{
				return;
			}
			Character localCharacter = HackLocal.LocalCharacter;
			Character closestToCrosshair = this.GetClosestToCrosshair();
			if (closestToCrosshair == null)
			{
				return;
			}
			if (!CVars.Aimbot.SilentAim)
			{
				this.heliosBoxPos = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
				this.AutoAimAtPlayer(localCharacter, closestToCrosshair);
				return;
			}
			if (this.SilentAim(localCharacter, closestToCrosshair))
			{
				Vector3 position = HackLocal.GetHeadBone(closestToCrosshair).transform.position;
				Vector3 vector = Camera.main.WorldToScreenPoint(position);
				if (vector.z > 0f)
				{
					vector.y = (float)Screen.height - (vector.y + 1f);
					this.heliosBoxPos = new Vector2(vector.x, vector.y);
				}
			}
		}
	}
}
