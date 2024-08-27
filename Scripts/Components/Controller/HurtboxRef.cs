using Godot;
using System;

[GlobalClass]
public partial class HurtboxRef : Resource
{
	public HurtboxRef(){}
	public HurtboxComponent Hurtbox;
	public CollisionObject2D DebugArea;
	[Export] public Color HurtboxColor;
}
