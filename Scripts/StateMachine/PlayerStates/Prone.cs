using Godot;
using System;

public partial class Prone : PlayerState
{

    //Similar to the idle state, but leads into different motions
     public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
        player.Velocity = player.Velocity with { X = 0};
        //player.animator.Play("Prone")


    }
    public override void StatePhysicsProcess(float delta)
    {
        base.StatePhysicsProcess(delta);
        player.MoveAndSlide();

        if(player.inputComponent.inputHorizontal != 0)
        {
            EmitSignal("Finshed", "Crawling");
        }
        else if(player.inputComponent.GetJumpInput())
        {
            EmitSignal("Finished", "Jumping");
        }
        else if(player.inputComponent.inputVector.Y < -1)
        {
            EmitSignal("Finished", "Idle");
        }
    }

}
