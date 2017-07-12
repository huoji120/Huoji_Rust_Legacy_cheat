using Rust_Jacked.Settings;
using System;
using UnityEngine;
namespace Rust_Jacked.Rendering
{
    public static class Canvas
	{
		[Flags]
		public enum TextFlags
		{
			TEXT_FLAG_NONE = 0,
			TEXT_FLAG_CENTERED = 1,
			TEXT_FLAG_OUTLINED = 2,
			TEXT_FLAG_DROPSHADOW = 3
		}
		private static float lastFrameTime = (float)Environment.TickCount;
		private static float framesPerSecond = 0f;
		private static float averageFramesPerSecond = 0f;
		private static float totalFramesPerSecond = 0f;
		private static Texture2D brandingTexture = null;
		private static Rect brandingRect;
		private static Texture2D drawingTex = null;
		private static Color lastTexColor;
		private static Material lineMaterial;
		public static void UpdateFPS()
		{
			float num = (float)Environment.TickCount;
			Canvas.totalFramesPerSecond = (Canvas.framesPerSecond + 0.1f) / ((num - Canvas.lastFrameTime) / 1000f);
			if (num - Canvas.lastFrameTime > 1000f)
			{
				Canvas.lastFrameTime = num;
				Canvas.framesPerSecond = 0f;
				Canvas.averageFramesPerSecond = Canvas.totalFramesPerSecond;
			}
			Canvas.framesPerSecond += 1f;
		}
       


        public static void DrawWatermark()
		{
			if (!CVars.Misc.ShowWatermark)
			{
				return;
			}



            /*
			if (Canvas.brandingTexture == null)
			{
				//Canvas.brandingTexture = new Texture2D(64, 64, TextureFormat.RGBA32, false);
				//Canvas.brandingTexture.LoadImage(Watermark.data);
				//Canvas.brandingRect = new Rect((float)(Screen.width / 2 - Canvas.brandingTexture.width / 2), 2f, (float)Canvas.brandingTexture.width, (float)Canvas.brandingTexture.height);
				return;
			}*/
            //GUI.DrawTexture(Canvas.brandingRect, Canvas.brandingTexture);
        }
		public static void DrawFPS()
		{
			//Canvas.DrawString(new Vector2(Canvas.brandingRect.xMax + 50f, 50f), new UColor(0, 255, 255, 255f).Get(), Canvas.TextFlags.TEXT_FLAG_OUTLINED, string.Format("øŸøŸ∆®—€Œ≈Œ≈ ÷: {0}", "303187947"));
		}
		public static void DrawString(Vector2 pos, Color color, Canvas.TextFlags flags, string text)
		{
			bool center = (flags & Canvas.TextFlags.TEXT_FLAG_CENTERED) == Canvas.TextFlags.TEXT_FLAG_CENTERED;
			if ((flags & Canvas.TextFlags.TEXT_FLAG_OUTLINED) == Canvas.TextFlags.TEXT_FLAG_OUTLINED)
			{
				Canvas.DrawStringInternal(pos + new Vector2(1f, 0f), Color.black, text, center);
				Canvas.DrawStringInternal(pos + new Vector2(0f, 1f), Color.black, text, center);
				Canvas.DrawStringInternal(pos + new Vector2(0f, -1f), Color.black, text, center);
			}
			if ((flags & Canvas.TextFlags.TEXT_FLAG_DROPSHADOW) == Canvas.TextFlags.TEXT_FLAG_DROPSHADOW)
			{
				Canvas.DrawStringInternal(pos + new Vector2(1f, 1f), Color.black, text, center);
			}
			Canvas.DrawStringInternal(pos, color, text, center);
		}
		public static Vector2 TextBounds(string text)
		{
			return new GUIStyle(GUI.skin.label)
			{
				fontSize = 13
			}.CalcSize(new GUIContent(text));
		}
		private static void DrawStringInternal(Vector2 pos, Color color, string text, bool center)
		{
			GUIStyle gUIStyle = new GUIStyle(GUI.skin.label);
			gUIStyle.normal.textColor = color;
			gUIStyle.fontSize = 13;
			if (center)
			{
				pos.x -= gUIStyle.CalcSize(new GUIContent(text)).x / 2f;
			}
			GUI.Label(new Rect(pos.x, pos.y, 264f, 20f), text, gUIStyle);
		}
		public static void DrawBox(Vector2 pos, Vector2 size, Color color)
		{
			if (!Canvas.drawingTex)
			{
				Canvas.drawingTex = new Texture2D(1, 1);
			}
			if (color != Canvas.lastTexColor)
			{
				Canvas.drawingTex.SetPixel(0, 0, color);
				Canvas.drawingTex.Apply();
				Canvas.lastTexColor = color;
			}
			GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), Canvas.drawingTex);
		}
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
		{
			if (!Canvas.lineMaterial)
			{
				Canvas.lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {   BindChannels { Bind \"Color\",color }   Blend SrcAlpha OneMinusSrcAlpha   ZWrite Off Cull Off Fog { Mode Off }} } }");
				Canvas.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
				Canvas.lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
			}
			Canvas.lineMaterial.SetPass(0);
			GL.Begin(1);
			GL.Color(color);
			GL.Vertex3(pointA.x, pointA.y, 0f);
			GL.Vertex3(pointB.x, pointB.y, 0f);
			GL.End();
		}
		public static void DrawBoxOutlines(Vector2 position, Vector2 size, float borderSize, Color color)
		{
			Canvas.DrawBox(new Vector2(position.x + borderSize, position.y), new Vector2(size.x - 2f * borderSize, borderSize), color);
			Canvas.DrawBox(new Vector2(position.x, position.y), new Vector2(borderSize, size.y), color);
			Canvas.DrawBox(new Vector2(position.x + size.x - borderSize, position.y), new Vector2(borderSize, size.y), color);
			Canvas.DrawBox(new Vector2(position.x + borderSize, position.y + size.y - borderSize), new Vector2(size.x - 2f * borderSize, borderSize), color);
		}
		public static void DrawCrosshair()
		{
            /*
			if (!CVars.Crosshair.Enable)
			{
				return;
			}
            */
			int style = CVars.Crosshair.Style;
			int color = CVars.Crosshair.Color;
			int opacity = CVars.Crosshair.Opacity;
			int size = CVars.Crosshair.Size;
			int num = Screen.width / 2;
			int num2 = Screen.height / 2;
			Color white = Color.white;
			switch (color)
			{
			case 0:
				white = new Color(225f, 255f, 0f, (float)opacity);
				break;
			case 1:
				white = new Color(255f, 0f, 0f, (float)opacity);
				break;
			case 2:
				white = new Color(50f, 205f, 50f, (float)opacity);
				break;
			case 3:
				white = new Color(0f, 255f, 255f, (float)opacity);
				break;
			case 4:
				white = new Color(255f, 255f, 255f, (float)opacity);
				break;
			case 5:
				white = new Color(0f, 0f, 0f, (float)opacity);
				break;
			}
			if (opacity != 255)
			{
				white.r /= 255f;
				white.g /= 255f;
				white.b /= 255f;
				white.a /= 255f;
			}
			switch (style)
			{
			case 0:
				Canvas.DrawLine(new Vector2((float)num, (float)(num2 - size)), new Vector2((float)num, (float)num2), white);
				Canvas.DrawLine(new Vector2((float)num, (float)num2), new Vector2((float)(num + size), (float)num2), white);
				Canvas.DrawLine(new Vector2((float)num, (float)num2), new Vector2((float)num, (float)(num2 + size)), white);
				Canvas.DrawLine(new Vector2((float)(num - size), (float)num2), new Vector2((float)num, (float)num2), white);
				return;
			case 1:
				Canvas.DrawBox(new Vector2((float)(num - size), (float)(num2 - 2)), new Vector2((float)(size * 2), 4f), Color.black);
				Canvas.DrawBox(new Vector2((float)(num - 2), (float)(num2 - size)), new Vector2(4f, (float)(size * 2)), Color.black);
				Canvas.DrawBox(new Vector2((float)(num - size + 1), (float)(num2 - 1)), new Vector2((float)(size * 2 - 2), 2f), white);
				Canvas.DrawBox(new Vector2((float)(num - 1), (float)(num2 - size + 1)), new Vector2(2f, (float)(size * 2 - 2)), white);
				return;
			case 2:
				Canvas.DrawLine(new Vector2((float)num, (float)(num2 - size)), new Vector2((float)num, (float)(num2 - 2)), white);
				Canvas.DrawLine(new Vector2((float)(num + 2), (float)num2), new Vector2((float)(num + size), (float)num2), white);
				Canvas.DrawLine(new Vector2((float)num, (float)(num2 + 2)), new Vector2((float)num, (float)(num2 + size)), white);
				Canvas.DrawLine(new Vector2((float)(num - size), (float)num2), new Vector2((float)(num - 2), (float)num2), white);
				return;
			case 3:
				Canvas.DrawLine(new Vector2((float)(num - size), (float)(num2 - size)), new Vector2((float)(num - 2), (float)(num2 - 2)), white);
				Canvas.DrawLine(new Vector2((float)(num - size), (float)(num2 + size)), new Vector2((float)(num - 2), (float)(num2 + 2)), white);
				Canvas.DrawLine(new Vector2((float)(num + size), (float)(num2 - size)), new Vector2((float)(num + 2), (float)(num2 - 2)), white);
				Canvas.DrawLine(new Vector2((float)(num + size), (float)(num2 + size)), new Vector2((float)(num + 2), (float)(num2 + 2)), white);
				return;
			case 4:
				Canvas.DrawBox(new Vector2((float)(num - size / 2), (float)(num2 - size / 2)), new Vector2((float)size, (float)size), white);
				return;
			default:
				return;
			}
		}
		public static void HeliosBox()
		{
			Canvas.HeliosBox((float)(Screen.width / 2), (float)(Screen.height / 2));
		}
		public static void HeliosBox(float sx, float sy)
		{
			Color color = new Color(255f, 255f, 0f);
			Canvas.DrawLine(new Vector2(sx - 8f, sy - 8f), new Vector2(sx + 8f, sy - 8f), color);
			Canvas.DrawLine(new Vector2(sx + 8f, sy - 8f), new Vector2(sx + 8f, sy + 8f), color);
			Canvas.DrawLine(new Vector2(sx - 8f, sy + 8f), new Vector2(sx + 8f, sy + 8f), color);
			Canvas.DrawLine(new Vector2(sx - 8f, sy - 8f), new Vector2(sx - 8f, sy + 8f), color);
		}
	}
}
