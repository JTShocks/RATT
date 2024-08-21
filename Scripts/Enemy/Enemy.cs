using Godot;
using System;

public partial class Enemy : CharacterBody2D
{

	[ExportSubgroup("Nodes")]
	[Export] public GravityComponent gravityComponent;
	[Export] public MovementComponent movementComponent;
	[Export] HealthComponent healthComponent;
	[Export] StateMachine stateMachine;

	float CriticalDamageMultiplier = 2f;

	//Things that all enemies need to have
	//Stats
	//Functions for what to do when they are alerted

	//Should not know exactly what state they are in

	const float MAX_AWARENESS = 100f;
	public float currentAwareness; //How much awareness the enemies have while
	public bool IsAwake =>  currentAwareness >= MAX_AWARENESS;




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach(HurtboxComponent hurtbox in FindChildren("*","HurtboxComponent"))
        {


            hurtbox.OnTakeDamage += TakeDamage;
			GD.Print("Grabbed the hurtbox: " + hurtbox.Name);
        }

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void TakeDamage(float damage, HurtboxComponent hurtbox)
	{
		float outputDamage = damage;
		//Is the hurtbox that got hit a critical spot
		if(hurtbox.IsCriticalZone)
		{
			outputDamage *= CriticalDamageMultiplier;

			//Check if the enemy's current state is sleeping. If YES, then take even more damage.
			if(stateMachine.state is Sleeping)
			{
				outputDamage *= CriticalDamageMultiplier;
			}
		}


		//Output the damage to the health component
		healthComponent.Damage(outputDamage);
		GD.Print(Name + " took damage!");
	}

	public void Attack()
	{

	}
}
