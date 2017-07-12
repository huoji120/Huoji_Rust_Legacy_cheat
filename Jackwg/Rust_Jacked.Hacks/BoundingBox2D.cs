using System;
using UnityEngine;
namespace Rust_Jacked.Hacks
{
	public class BoundingBox2D
	{
		public float Width
		{
			get;
			set;
		}
		public float Height
		{
			get;
			set;
		}
		public float X
		{
			get;
			set;
		}
		public float Y
		{
			get;
			set;
		}
		public bool IsValid
		{
			get;
			set;
		}
		public BoundingBox2D(Character character)
		{
			Vector3 position = character.transform.position;
			Vector3 position2 = HackLocal.GetHeadBone(character).transform.position;
			Vector3 vector = Camera.main.WorldToScreenPoint(position2);
			Vector3 vector2 = Camera.main.WorldToScreenPoint(position);
			if (vector.z > 0f && vector2.z > 0f)
			{
				vector.y = (float)Screen.height - (vector.y + 1f);
				vector2.y = (float)Screen.height - (vector2.y + 1f);
				this.Height = vector2.y + 10f - vector.y;
				this.Width = this.Height / 2f;
				this.X = vector.x - this.Width / 2f;
				this.Y = vector.y;
				this.IsValid = true;
				return;
			}
			this.IsValid = false;
		}
	}
}
