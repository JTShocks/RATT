using Godot;
using System;

public partial class MovementComponent : Node
{
	[ExportSubgroup("Settings")]
	[Export] float moveSpeed = 200;
	public float movementDirection;

	public void HandleHorizontalMovement(CharacterBody2D body, float direction)
	{

		if(direction > 0 )
		{
			direction = 1;
		}
		else if(direction < 0)
		{
			direction = -1;
		}
		movementDirection = direction;
		body.Velocity = body.Velocity with {X = direction * moveSpeed};
	}
}
