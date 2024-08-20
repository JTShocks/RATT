using Godot;
using System;

public partial class Enemy : CharacterBody2D
{


	//Things that all enemies need to have
	//Stats
	//Functions for what to do when they are alerted

	//Should not know exactly what state they are in

	public float currentAwareness; //How much awareness the enemies have while 




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}



	public void OnGainAwareness()
	{

	}

	public void Attack()
	{

	}
}
