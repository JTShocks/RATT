using Godot;
using System;


public partial class Awakened : EnemyState
{

    [Export] float wakeUpTime;
    Timer wakeUpTimer;

    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);

        wakeUpTimer = new Timer()
        {
                WaitTime = wakeUpTime
        };
        AddChild(wakeUpTimer);
         //Have the timer delete itself when it finishes
        wakeUpTimer.Start();

    }

    public override void StatePhysicsProcess(float delta)
    {
        base.StatePhysicsProcess(delta);


        //When the enemy takes damage, go to the stagger state


        if(wakeUpTimer.TimeLeft == 0)
        {
            wakeUpTimer.QueueFree();
            EmitSignal("Finished", "Attack"); // Have the enemy go into the attack state
        }
        else if(enemy.IsStaggered)
        {
            EmitSignal("Finished", "Stagger");
        }
    }

}
