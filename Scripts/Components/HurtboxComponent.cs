using Godot;
using System;

public partial class HurtboxComponent : Area2D
{

	[Export] bool IsCriticalZone;
	[Export] int CriticalDamageMultiplier = 2;
	[Export] int DamageReduction = 0;


    [ExportSubgroup("Nodes")]
    [Export] public HealthComponent healthComponent;



    public override void _Ready()
    {
        base._Ready();
    }



    public void OnGetHit(float damage)
    {

		float incomingDamage = damage - DamageReduction;

		if(IsCriticalZone)
		{
			incomingDamage *= CriticalDamageMultiplier;
		}

		healthComponent.Damage(incomingDamage);

    }



}
