using Godot;
using System;

public partial class ElevatorController : Node2D
{

	[Export] bool startOnPlayerEnter;
	

	//Can make a resources called "ElevatorBehaviour" that the controller looks at to determine when to start and stop the elevator
	//So if it requires a switch, just use the "ElevatorNeedsSwitch:ElevatorBehaviour" resource

	Path2D elevatorPath;
	public Elevator elevator;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		elevatorPath = GetNode<Path2D>("Path2D");
		elevator = elevatorPath.GetNode<Elevator>("Elevator");

		//Can subscribe to the events of the elevator for when the player enters it
		//This can be used to have it automatically work when the player steps in.

		if(startOnPlayerEnter)
		{
			elevator.PlayerEnteredElevator += StartElevator;
		}

		
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
