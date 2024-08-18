using Godot;
using System;

public partial class Jumping : PlayerState
{
    //NOTE: Perhaps have the states find reference to any component they might need

    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
        //TODO: Add in call to play the necessary animation
       
    }

    public override void StatePhysicsProcess(float delta)
    {
        base.StatePhysicsProcess(delta);
        player.jumpComponent.HandleJump(player, true);
        player.MoveAndSlide();

        if(player.IsOnFloor())
        {
            if(Mathf.IsEqualApprox(player.inputComponent.inputHorizontal, 0.0f))
            {
                EmitSignal("Finished", "Idle");
            }
            else
            {
                EmitSignal("Finished", "Running");
            }
        }
        else if(player.inputComponent.inputVector.Y > 0)
        {
            //The player is trying to dive out of the state
            EmitSignal("Finished", "Diving");
        }

    }
}
