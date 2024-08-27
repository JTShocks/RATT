using Godot;
using System;

public partial class Enemy : CharacterBody2D
{

	[ExportSubgroup("Nodes")]
	[Export] public GravityComponent gravityComponent;
	[Export] public MovementComponent movementComponent;
	[Export] public HealthComponent healthComponent;
	[Export] HurtboxController hurtboxController;

	float CriticalDamageMultiplier = 2f;

	//Things that all enemies need to have
	//Stats
	//Functions for what to do when they are alerted

	//Should not know exactly what state they are in

	const float MAX_AWARENESS = 100f;
	public float currentAwareness; //How much awareness the enemies have while
	public bool IsAwake =>  currentAwareness >= MAX_AWARENESS;

	public bool IsVulnerable; //This determines if the enemy takes extra damage from a given state

	[Export] float staggerLimit = 100;
	public float currentStagger;
	public bool IsStaggered => currentStagger >= staggerLimit;





	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


		foreach(HurtboxRef reference in hurtboxController.Hurtboxes)
        {
            reference.Hurtbox.OnTakeDamage += TakeDamage;
			GD.Print("Grabbed the hurtbox: " + reference.Hurtbox.Name);
        }

		healthComponent.Died += OnDeath;

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

		}
		if(IsVulnerable)
		{
			outputDamage *= CriticalDamageMultiplier;
		}


		//Output the damage to the health component
		healthComponent.Damage(outputDamage);
		ChangeAwareness(100);
		GD.Print(Name + " took damage!");
	}

	void ChangeAwareness(int value)
	{
		currentAwareness += value;

	}

	public virtual void OnAttack()
	{

	}

	public virtual void OnDeath()
	{
		GD.Print(Name + " has died.");
		QueueFree();
	}
}
