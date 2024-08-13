using Godot;
using System;

public partial class GravityComponent : Node
{
	[ExportSubgroup("Settings")]
	[Export] float gravity = 1000.0f;
	[Export] float gravityScale = 1.0f;

	public bool isFalling = false;

	public void HandleGravity(CharacterBody2D body, float delta)
	{
		if(!body.IsOnFloor())
		{
			body.Velocity += Vector2.Down * gravity * gravityScale * delta;
		}
		isFalling = body.Velocity.Y > 0 && !body.IsOnFloor();

		if(isFalling)
		{
			gravityScale = 2;
		}
		else
		{
			gravityScale = 1;
		}
	}
}

