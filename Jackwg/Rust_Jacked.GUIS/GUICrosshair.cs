using Rust_Jacked.Hacks;
using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.GUIS
{
	internal class GUICrosshair : MonoBehaviour
	{
		public static Rect startRect = new Rect(100f, 150f, 215f, 140f);
		private void Start()
		{
			GUICrosshair.startRect.x = GUIMisc.startRect.xMax + 25f;
		}
		private void OnGUI()
		{
			if (HackLocal.ShowMenu)
			{
				GUICrosshair.startRect = GUI.Window(1, GUICrosshair.startRect, new GUI.WindowFunction(this.DoMyWindow), "QQ1296564236");
			}
		}
		private void DoMyWindow(int windowID)
		{

            
			CVars.Crosshair.Enable = GUI.Toggle(new Rect(10f, 20f, 160f, 20f), CVars.Crosshair.Enable, "准心开关");
			GUI.Label(new Rect(25f, 45f, 120f, 20f), string.Format("风格 = ({0})", CVars.Crosshair.Style));
			CVars.Crosshair.Style = (int)GUI.HorizontalSlider(new Rect(125f, 50f, 80f, 12f), (float)CVars.Crosshair.Style, 0f, 4f);
			GUI.Label(new Rect(25f, 65f, 120f, 20f), string.Format("颜色 = ({0})", CVars.Crosshair.Color));
			CVars.Crosshair.Color = (int)GUI.HorizontalSlider(new Rect(125f, 70f, 80f, 12f), (float)CVars.Crosshair.Color, 0f, 5f);
			GUI.Label(new Rect(25f, 85f, 120f, 20f), string.Format("透明 = ({0})", CVars.Crosshair.Opacity));
			CVars.Crosshair.Opacity = (int)GUI.HorizontalSlider(new Rect(125f, 90f, 80f, 12f), (float)CVars.Crosshair.Opacity, 1f, 255f);
			GUI.Label(new Rect(25f, 105f, 120f, 20f), string.Format("大小 = ({0})", CVars.Crosshair.Size));
			CVars.Crosshair.Size = (int)GUI.HorizontalSlider(new Rect(125f, 110f, 80f, 12f), (float)CVars.Crosshair.Size, 1f, 25f);
			CVars.Crosshair.Style = Mathf.RoundToInt((float)CVars.Crosshair.Style);
			CVars.Crosshair.Color = Mathf.RoundToInt((float)CVars.Crosshair.Color);
			CVars.Crosshair.Opacity = Mathf.RoundToInt((float)CVars.Crosshair.Opacity);
			CVars.Crosshair.Size = Mathf.RoundToInt((float)CVars.Crosshair.Size);
            
			GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}
	}
}
