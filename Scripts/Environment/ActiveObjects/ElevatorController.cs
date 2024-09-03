using Godot;
using System;

public partial class ElevatorController : Node2D
{

	Path2D elevatorPath;
	public Elevator elevator;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		elevatorPath = GetNode<Path2D>("Path2D");
		elevator = elevatorPath.GetNode<Elevator>("Elevator");

		//Can subscribe to the events of the elevator for when the player enters it
		//This can be used to have it automatically work when the player steps in.
	}
	public void StartElevator()
	{
		elevator.Start();
	}

	public void StopElevaor()
	{
		elevator.Stop();
	}
}
