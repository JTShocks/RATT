using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{


	[ExportSubgroup("Character Booleans")]
	[Export] public bool CanMove;
	[Export] public bool CanJump;
	[Export] public bool CanShoot;

	bool isFacingLeft = false;

	[ExportCategory("Nodes")]

	[Export] public MovementComponent movementComponent;
	[Export] public GravityComponent gravityComponent;
	[Export] HealthComponent healthComponent;
	[Export] public InputComponent inputComponent;

	[Export] Node2D body;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Vector2 mousePos = GetGlobalMousePosition();

		if(mousePos.X < GlobalPosition.X && !isFacingLeft)
		{
			//Player is looking left
			isFacingLeft = true;
			FlipPlayer();
		}
		else if(mousePos.X > GlobalPosition.X && isFacingLeft)
		{
			isFacingLeft = false;
			FlipPlayer();
		}


	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		gravityComponent.HandleGravity(this, (float)delta);

		if(CanMove)
		{
			//movementComponent.HandleHorizontalMovement(this, inputComponent.inputVector.X);
		}

		if(CanShoot)
		{

		}

		if(CanJump)
		{

		}

    }


    void GetInput()
	{

	}

	void FlipPlayer()
	{
		body.ApplyScale(new Vector2(-1,1));
	}
}
