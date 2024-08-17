using Godot;
using Godot.Collections;
using System;
public partial class StateMachine : Node
{

    [Export] State initialState = null;

    [ExportSubgroup("Debug")]
    [Export] Label currentStateLabel;

    State state {get
        {
            if(initialState != null)
            {
                return initialState;
            }
            else
            {
                return GetChild<State>(0);
            }
        } set{}}


    public override async void _Ready()
    {
        base._Ready();
        foreach(State stateNode in FindChildren("*", "State"))
        {
            stateNode.Finished += TransitionToNextState;
        }

        await ToSignal(Owner, SignalName.Ready);
        state.OnEnter("");

    }

    void TransitionToNextState(string targetStatePath)
    {
        if(!HasNode(targetStatePath))
        {
            GD.PrintErr(Owner.Name + ": Trying to transition to state " + targetStatePath + " but it does not exist.");
            return;
        }

        var previousStatePath = state.Name;
        state.OnExit();
        state = GetNode<State>(targetStatePath);
        state.OnEnter(previousStatePath);
    }

    public override void _Process(double delta)
    {
        state.StateProcess((float)delta);
        currentStateLabel.Text = "State: " + state.Name;

    }
    public override void _PhysicsProcess(double delta)
    {
        state.StatePhysicsProcess((float)delta);
        
    }
}
