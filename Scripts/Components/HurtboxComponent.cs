using Godot;
using System;

[GlobalClass]
public partial class HurtboxComponent : StaticBody2D
{


  [ExportSubgroup("Debug")]
    [Export] Color hurtboxColor;
	[Export] public bool IsCriticalZone {get; private set;}
	[Export] int DamageReduction = 0;



  public event Action<float, HurtboxComponent> OnTakeDamage;


    public void OnGetHit(float damage)
    {
      OnTakeDamage.Invoke(damage, this);
      GD.Print(Name + " got hit! \n Owner is: " + Owner.Name);

    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(Engine.IsEditorHint())
        {
          CollisionShape2D hurtboxShape = GetNode<CollisionShape2D>("CollisionShape2D");
          hurtboxShape.DebugColor = hurtboxColor;
        }
    }



}
