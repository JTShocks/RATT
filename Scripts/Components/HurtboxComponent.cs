using Godot;
using System;

[GlobalClass]
public partial class HurtboxComponent : CollisionShape2D
{

	[Export] public bool IsCriticalZone {get; private set;}
	[Export] int DamageReduction = 0;



  public event Action<float, HurtboxComponent> OnTakeDamage;


    public void OnGetHit(float damage)
    {
      OnTakeDamage.Invoke(damage, this);
      GD.Print(Name + " got hit! \n Owner is: " + Owner.Name);

    }



}
