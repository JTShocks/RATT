using Godot;
using System;

public partial class StateDisplayGUI : Label
{

    [Export] StateMachine stateMachine;

    public override void _Process(double delta)
    {
        base._Process(delta);

        Text = "State: " + stateMachine.state.Name;
    }
}
