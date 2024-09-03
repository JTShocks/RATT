using Godot;
using System;

public partial class Elevator : PathFollow2D
{

	[ExportSubgroup("Elevator Values")]
	[Export] float moveSpeed;
	[Export] bool startAtBottom;
	//Elevator should not have a reference to it's switch, that should only be done during the scene
	bool isMoving;
	Area2D elevatorZone;
	Vector2 moveDirection = Vector2.Down;
	// Called when the node enters the scene tree for the first time.

	PlayerController player;

	public event Action PlayerEnteredElevator;
	public event Action PlayerExitedElevator;
	public override void _Ready()
	{
		//Check if the player has officially entered the elevator
		//Sets up a protection so the player can't accidently start the elevator when they are outside it
		elevatorZone.BodyEntered += OnPlayerEnterElevator;
		elevatorZone.BodyExited += OnPlayerExitElevator;

		if(startAtBottom)
		{
			ProgressRatio = 1;
		}
		else
		{
			ProgressRatio = 0;
		}
		Start();


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		if(isMoving)
		{
			//Advance the elevator either down to it's last position, or up to it's start position
			Progress += moveDirection.Y * moveSpeed * (float)delta;
			if(ProgressRatio <= 0 || ProgressRatio >= 1)
			{
				Stop();
			}
			
		}
	}


	public void Start()
	{
		if(ProgressRatio == 1)
		{
			//move the elevator up
			moveDirection = Vector2.Up;
		}
		else
		{
			//move the elevator down
			moveDirection = Vector2.Down;
		}
		isMoving = true;

	}

	public void Stop()
	{
		isMoving = false;
	}

	public void OnPlayerEnterElevator(Node2D body)
	{
		if(body is PlayerController p)
		{
			player = p;
			//Save the current player in the elevator

			PlayerEnteredElevator.Invoke();
		}
	}
	public void OnPlayerExitElevator(Node2D body)
	{
		if(body is PlayerController p)
		{
			//What happens when the player leaves the elevator range
			PlayerExitedElevator.Invoke();
		}
	}
}
