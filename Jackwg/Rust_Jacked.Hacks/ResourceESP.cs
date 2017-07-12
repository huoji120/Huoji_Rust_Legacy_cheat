using Rust_Jacked.Rendering;
using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	public class ResourceESP : MonoBehaviour
	{
		private UColor resourceColor = new UColor(255f, 255f, 0f, 255f);
		private void OnGUI()
		{
			if (Event.current.type == EventType.Repaint && HackLocal.IsIngame)
			{
				try
				{
					this.DrawResources();
					this.DrawAnimals();
				}
				catch
				{
				}
			}
		}
        private void DrawResources()
		{
			if (!CVars.ESP.DrawResources)
			{
				return;
			}
			UnityEngine.Object[] resourceObjects = HackLocal.ResourceObjects;
			for (int i = 0; i < resourceObjects.Length; i++)
			{
				UnityEngine.Object @object = resourceObjects[i];
                if (@object != null)
                {
                    ResourceObject resourceObject = (ResourceObject)@object;
                    string arg = resourceObject.name.Replace("(Clone)", "");
                    Vector3 vector = Camera.main.WorldToScreenPoint(resourceObject.transform.position);
                    if (vector.z > 0f)
                    {
                        vector.y = (float)Screen.height - (vector.y + 1f);
                        Canvas.DrawString(new Vector2(vector.x, vector.y), this.resourceColor.Get(), Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, string.Format("{0} [{1}]", arg, (int)vector.z));


                    }
                }
			}
		}
		private void DrawAnimals()
		{
			if (!CVars.ESP.DrawAnimals)
			{
				return;
			}
			foreach (Character current in HackLocal.GetAnimalList())
			{
				string arg = current.name.Replace("_A", "").Replace("(Clone)", "");
				Vector3 vector = Camera.main.WorldToScreenPoint(current.transform.position);
				if (vector.z > 0f && current.transform.position.y > 100f)
				{
					vector.y = (float)Screen.height - (vector.y + 1f);
					Canvas.DrawString(new Vector2(vector.x, vector.y), Color.gray, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, string.Format("{0} [{1}]", arg, (int)vector.z));
				}
			}
		}
	}
}
