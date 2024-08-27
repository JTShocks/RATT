using Godot;
using System;

	public partial class HurtboxRef : Resource
	{
		public HurtboxRef(){}
		public HurtboxComponent Hurtbox;
		public CollisionObject2D DebugArea;
		[Export] public Color HurtboxColor;
	}
