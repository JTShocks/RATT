using Godot;
using System;

public partial class HurtboxComponent : Area2D
{

	[Export] public bool IsCriticalZone {get; private set;}
	[Export] int CriticalDamageMultiplier = 2;
	[Export] int DamageReduction = 0;



  public event Action<float, HurtboxComponent> OnTakeDamage;



    public override void _Ready()
    {
        base._Ready();
    }



    public void OnGetHit(float damage)
    {
      OnTakeDamage.Invoke(damage, this);

    }



}
