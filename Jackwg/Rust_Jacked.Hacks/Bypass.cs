using System;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	internal class Bypass : MonoBehaviour
	{
		private void Update()
		{
			if (SteamClient.steamClientObject != null)
			{
				SteamClient.steamClientObject.SetActive(false);
				SteamClient.SteamClient_Cycle();
			}
		}
	}
}
