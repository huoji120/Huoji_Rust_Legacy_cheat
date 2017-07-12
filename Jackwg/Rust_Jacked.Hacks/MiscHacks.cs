using Rust_Jacked.Settings;
using System;
using uLink;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	internal class MiscHacks : UnityEngine.MonoBehaviour
	{
		private CCMotor.Jumping? defaultJumping = null;
		private CCMotor.Movement? defaultMovement = null;
		private GameObject lightGameObject;

        public TOD_Sky Sky_Obj;
        private void Start()
		{
			this.lightGameObject = new GameObject();
			Light light = this.lightGameObject.AddComponent<Light>();
			light.type = LightType.Point;
			light.range = 1000f;
			light.intensity = 1f;
			light.color = Color.white;
			this.lightGameObject.SetActive(false);
			UnityEngine.Object.DontDestroyOnLoad(this.lightGameObject);
		}
		private void Update()
		{
			if (!HackLocal.IsIngame)
			{
				return;
			}
			try
			{
				this.LightHack();
				this.MotorHacks();
				this.NoRecoil();
				this.NoReload();
			}
			catch
			{
			}
		}
		private void LightHack()
		{
            /*
			this.lightGameObject.SetActive(CVars.Misc.LightHack);
			this.lightGameObject.transform.position = Camera.main.transform.position;
            */
            if (CVars.Misc.LightHack)
            {
                Sky_Obj = (TOD_Sky)FindObjectOfType(typeof(TOD_Sky));
                Sky_Obj.Cycle.Hour = 12f;
            }

        }
		private void MotorHacks()
		{
			HumanController localController = HackLocal.LocalController;
			Character localCharacter = HackLocal.LocalCharacter;
			CCMotor ccmotor = localController.ccmotor;
			if (ccmotor != null)
			{
				if (!this.defaultJumping.HasValue)
				{
					this.defaultJumping = new CCMotor.Jumping?(ccmotor.jumping.setup);
				}
				else
				{
					ccmotor.minTimeBetweenJumps = 0.1f;
					ccmotor.jumping.setup.baseHeight = this.defaultJumping.Value.baseHeight * CVars.Misc.JumpModifer;
				}
				if (!this.defaultMovement.HasValue)
				{
					this.defaultMovement = new CCMotor.Movement?(ccmotor.movement.setup);
				}
				else
				{
					ccmotor.movement.setup.maxForwardSpeed = this.defaultMovement.Value.maxForwardSpeed * CVars.Misc.SpeedModifer / 10f;
					ccmotor.movement.setup.maxSidewaysSpeed = this.defaultMovement.Value.maxSidewaysSpeed * CVars.Misc.SpeedModifer / 10f;
					ccmotor.movement.setup.maxBackwardsSpeed = this.defaultMovement.Value.maxBackwardsSpeed * CVars.Misc.SpeedModifer / 10f;
					ccmotor.movement.setup.maxGroundAcceleration = this.defaultMovement.Value.maxGroundAcceleration * CVars.Misc.SpeedModifer / 10f;
					ccmotor.movement.setup.maxAirAcceleration = this.defaultMovement.Value.maxAirAcceleration * CVars.Misc.SpeedModifer / 10f;
					if (CVars.Misc.NoFallDamage)
					{
						ccmotor.movement.setup.maxFallSpeed = 17f;
					}
					else
					{
						ccmotor.movement.setup.maxFallSpeed = this.defaultMovement.Value.maxFallSpeed;
					}
				}
				if (CVars.Misc.FlyHack)
				{
					ccmotor.velocity = Vector3.zero;
					Vector3 forward = localCharacter.eyesAngles.forward;
					Vector3 right = localCharacter.eyesAngles.right;
					if (!ChatUI.IsVisible())
					{
						if (Input.GetKey(KeyCode.W))
						{
							ccmotor.velocity += forward * (ccmotor.movement.setup.maxForwardSpeed * 3f);
						}
						if (Input.GetKey(KeyCode.S))
						{
							ccmotor.velocity -= forward * (ccmotor.movement.setup.maxBackwardsSpeed * 3f);
						}
						if (Input.GetKey(KeyCode.A))
						{
							ccmotor.velocity -= right * (ccmotor.movement.setup.maxSidewaysSpeed * 3f);
						}
						if (Input.GetKey(KeyCode.D))
						{
							ccmotor.velocity += right * (ccmotor.movement.setup.maxSidewaysSpeed * 3f);
						}
						if (Input.GetKey(KeyCode.Space))
						{
							ccmotor.velocity += Vector3.up * (this.defaultMovement.Value.maxAirAcceleration * 3f);
						}
					}
					if (ccmotor.velocity == Vector3.zero)
					{
						ccmotor.velocity += Vector3.up * (ccmotor.settings.gravity * Time.deltaTime * 0.5f);
					}
				}
			}
		}
		private void NoRecoil()
		{
			if (!CVars.Misc.NoRecoil)
			{
				return;
			}
			HumanController localController = HackLocal.LocalController;
			InventoryItem currentEquippedItem = HackLocal.GetCurrentEquippedItem(localController);
			if (currentEquippedItem != null && !(currentEquippedItem is MeleeWeaponItem<MeleeWeaponDataBlock>))
			{
				BulletWeaponItem<BulletWeaponDataBlock> bulletWeaponItem = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
				if (bulletWeaponItem != null)
				{
					bulletWeaponItem.datablock.bulletRange = 300f;
					bulletWeaponItem.datablock.recoilPitchMin = 0f;
					bulletWeaponItem.datablock.recoilPitchMax = 0f;
					bulletWeaponItem.datablock.recoilYawMin = 0f;
					bulletWeaponItem.datablock.recoilYawMax = 0f;
					bulletWeaponItem.datablock.aimSway = 0f;
					bulletWeaponItem.datablock.aimSwaySpeed = 0f;
				}
			}
			CameraMount componentInChildren = localController.GetComponentInChildren<CameraMount>();
			if (componentInChildren != null)
			{
				HeadBob component = componentInChildren.GetComponent<HeadBob>();
				if (component != null)
				{
					component.aimRotationScalar = 0f;
					component.viewModelRotationScalar = 0f;
				}
			}
		}
		private void NoReload()
		{
			if (!CVars.Misc.NoReload)
			{
				return;
			}
			HumanController localController = HackLocal.LocalController;
			InventoryItem currentEquippedItem = HackLocal.GetCurrentEquippedItem(localController);
			if (currentEquippedItem != null && currentEquippedItem is BulletWeaponItem<BulletWeaponDataBlock>)
			{
				BulletWeaponItem<BulletWeaponDataBlock> bulletWeaponItem = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
				if (bulletWeaponItem.clipAmmo <= 1)
				{
					bulletWeaponItem.itemRepresentation.Action(3, uLink.RPCMode.Server);
				}
			}
		}
	}
}
