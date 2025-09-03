using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    /// <summary>
    /// Vectors mathematics and constants
    /// </summary>
    [InitializeWithSceneMaster]
	public class Vecteur : dat
	{
		public static Vecteur o;

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
		protected override void OnStructured()
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
		public static Vector3 LDir(Quaternion Rotation, Vector3 Direction) => Rotation * Direction;

		/// <summary> get a vector rotated by quaternion rotation </summary>
		public static Vector3 LDir(Vector3 rotation, Vector3 Direction) => LDir(Quaternion.Euler(rotation), Direction);
		
		/// <summary> get a vector rotated by quaternion rotation </summary>
		public static Vector3 LDir(float rotation, Vector3 Direction) => LDir(Quaternion.Euler(new Vector3(0, rotation, 0)), Direction);

		/// <summary> get y angle when seeing from position to another position </summary>	
		public static float RotDirectionY(Vector3 from, Vector3 to) => Quaternion.LookRotation(to - from).eulerAngles.y;

		/// <summary> get euleur angle when seeing from position to another position </summary>	
		public static Vector3 RotDirection(Vector3 from, Vector3 to) => Quaternion.LookRotation(to - from).eulerAngles;

		public static Vector3 Forward(Quaternion rotation) => rotation * Vector3.forward;
		public static Quaternion RotDirectionQuaternion(Vector3 from, Vector3 to) => Quaternion.LookRotation(to - from);
	}

	public static class Vector3Extensions
	{
		public static Vector3 OnlyY(this Vector3 V) => new Vector3(0, V.y, 0);
		public static Vector3 OnlyZX(this Vector3 V) => new Vector3(V.x, 0, V.z);

		public static Vector3 Flat(this Vector3 V) => new Vector3(V.x, 0, V.z);
	}

	public static class QuaternionExtensions
	{
		public static Quaternion AppliedAfter(this Quaternion Q, Vector3 V) => Q * Quaternion.Euler(V);
	}

	public static class CollisionExtensions
	{
		public static int id(this Collision c) => c.collider.gameObject.GetInstanceID();
	}

	public static class ColliderExtensions
	{
		public static int id(this Collider c) => c.gameObject.GetInstanceID();
	}
}