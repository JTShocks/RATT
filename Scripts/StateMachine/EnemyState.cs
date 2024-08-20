using Godot;
using System;

[GlobalClass]
public partial class EnemyState : State
{
    
    [Export] public Enemy enemy;

    public override async void _Ready()
    {
        base._Ready();

        await ToSignal(Owner, SignalName.Ready);
        enemy = GetOwnerOrNull<Enemy>();
        if(enemy == null)
        {
            GD.PrintErr("The EnemyState state type must be used only in enemy scene. Owner must be a Enemy node.");
        }
    }
}
