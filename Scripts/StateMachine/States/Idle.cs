using Godot;
using System;

public partial class Idle : PlayerState
{
    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
        player.Velocity = player.Velocity with { X = 0};
        //player.animator.Play("Idle")


    }
    public override void StatePhysicsProcess(float delta)
    {
        base.StatePhysicsProcess(delta);
        player.MoveAndSlide();

        if(player.inputComponent.inputHorizontal != 0)
        {
            EmitSignal(SignalName.Finished, "Running");
        }
    }

}
