using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    /// <summary>
    /// Vectors mathematics and constants
    /// </summary>
    [superstar]
	public class vecteur : moon
	{
		public static vecteur o;

		// layer mask constant
		public static LayerMask TempLayer;
		public static LayerMask Solid;
		public static LayerMask Attack;
		public static LayerMask Character;
		public static LayerMask SolidCharacterAttack;
		public static LayerMask SolidCharacter;
		public static LayerMask Trigger;

		// layer hardcoded constant
		public static readonly int CHARACTER = 10;
		public static readonly int ATTACK = 11;
		public static readonly int SOLID = 9;
		public static readonly int TRIGGER = 12;

		public static readonly float Drag = 1;

		// initialisation
		protected override void _ready()
		{
			o = this;

			TempLayer = LayerMask.GetMask("temp");
			Solid = LayerMask.GetMask("solid");
			Attack = LayerMask.GetMask("attack");
			Character = LayerMask.GetMask("character");
			SolidCharacter = LayerMask.GetMask("solid", "character");
			SolidCharacterAttack = LayerMask.GetMask("solid", "character", "attack");
			Trigger = LayerMask.GetMask("trigger");
		}

		/// <summary> get a vector rotated by quaternion rotation </summary>
		public static Vector3 ldir(Quaternion Rotation, Vector3 Direction) => Rotation * Direction;

		/// <summary> get a vector rotated by quaternion rotation </summary>
		public static Vector3 ldir(Vector3 rotation, Vector3 Direction) => ldir(Quaternion.Euler(rotation), Direction);
		
		/// <summary> get a vector rotated by quaternion rotation </summary>
		public static Vector3 ldir(float rotation, Vector3 Direction) => ldir(Quaternion.Euler(new Vector3(0, rotation, 0)), Direction);

		/// <summary> get y angle when seeing from position to another position </summary>	
		public static float rot_direction_y (Vector3 from, Vector3 to) => Quaternion.LookRotation(to - from).eulerAngles.y;

		/// <summary> get euleur angle when seeing from position to another position </summary>	
		public static Vector3 rot_direction (Vector3 from, Vector3 to) => Quaternion.LookRotation(to - from).eulerAngles;

		public static Vector3 forward (Quaternion rotation) => rotation * Vector3.forward;
		public static Quaternion rot_direction_quaternion(Vector3 from, Vector3 to) => Quaternion.LookRotation(to - from);
	}

	public static class Vector3Extensions
	{
		public static Vector3 y (this Vector3 V) => new Vector3(0, V.y, 0);
		public static Vector3 xz (this Vector3 V) => new Vector3(V.x, 0, V.z);
	}

	public static class QuaternionExtensions
	{
		public static Quaternion applied_after (this Quaternion Q, Vector3 V) => Q * Quaternion.Euler(V);
	}

	public static class CollisionExtensions
	{
		public static int id(this Collision c) => c.collider.gameObject.GetInstanceID();
	}

	public static class ColliderExtensions
	{
		public static int id(this Collider c) => c.gameObject.GetInstanceID();
	}

	// character sorting by average angle and distance
	public class SortDistanceA : IComparer < warrior >
	{
		float y;
		Vector3 pos;
		float distance;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="y"> euleur y of character's direction </param>
		/// <param name="pos"> the character's transform  </param>
		public SortDistanceA (float y, Vector3 pos, float distance)
		{
			this.y = y;
			this.pos = pos;
			this.distance = distance;
		}

		// compare by average distance and average angle
		public int Compare ( warrior x, warrior y )
		{
			float AngleDistanceRatio = Mathf.Abs(Mathf.DeltaAngle(this.y, vecteur.rot_direction_y(pos, x.skin.position))) / 180;
			float DistanceRatio = Vector3.Distance(pos, x.skin.position) / distance;
			float xAverage = (AngleDistanceRatio + DistanceRatio) / 2;
			AngleDistanceRatio = Mathf.Abs(Mathf.DeltaAngle(this.y, vecteur.rot_direction_y(pos, y.skin.position))) / 180;
			DistanceRatio = Vector3.Distance(pos, y.skin.position) / distance;
			float yAverage = (AngleDistanceRatio + DistanceRatio) / 2;
			return (int)Mathf.Sign(xAverage - yAverage);
		}
	}
}