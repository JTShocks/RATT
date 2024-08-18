using Godot;
using System;

public partial class JumpComponent : Node
{
	[ExportSubgroup("Settings")]
	[Export] float jumpForce = 300;

	public bool isJumping = false;

	public void HandleJump(CharacterBody2D body, bool wantToJump)
	{
		if(wantToJump && body.IsOnFloor())
		{
			body.Velocity += Vector2.Up * jumpForce;
		}
		isJumping = body.Velocity.Y < 0 && !body.IsOnFloor();
	}
}
