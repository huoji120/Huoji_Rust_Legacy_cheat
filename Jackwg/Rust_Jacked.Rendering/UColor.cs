using System;
using UnityEngine;
namespace Rust_Jacked.Rendering
{
	public class UColor
	{
		private Color color;
		public UColor(float r, float g, float b, float a = 255f)
		{
			this.color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);
		}
		public Color Get()
		{
			return this.color;
		}
	}
}
