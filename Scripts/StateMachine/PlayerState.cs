using Godot;
using System;

[GlobalClass]
public partial class PlayerState : State
{

    [Export] public PlayerController player;

    public override async void _Ready()
    {
        base._Ready();

        await ToSignal(Owner, SignalName.Ready);
        player = GetOwnerOrNull<PlayerController>();
        if(player == null)
        {
            GD.PrintErr("The PlayerState state type must be used only in player scene. Owner must be a PlayerController node.");
        }
    }
}
