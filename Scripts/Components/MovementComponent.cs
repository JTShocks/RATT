using Godot;
using System;

public partial class MovementComponent : Node
{

	const float BASE_MOVE_SPEED = 200f;
	[ExportSubgroup("Settings")]
	[Export] float moveSpeed = 200;
	[Export] float acceleration = 0.5f;
	public float movementDirection;

	public void HandleHorizontalMovement(CharacterBody2D body, float direction, float speed = 0)
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


		//If there is no speed inputed, use the component's set movespeed

		if(speed == 0)
		{
			speed = moveSpeed;
		}

		
		body.Velocity = body.Velocity with {X = Mathf.Lerp(body.Velocity.X, direction * speed, acceleration)};
		body.Velocity = body.Velocity with {X = Mathf.Clamp(body.Velocity.X, -moveSpeed, moveSpeed)};
	}
}
