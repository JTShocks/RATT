using Godot;
using Godot.Collections;
using System;
public partial class StateMachine : Node
{

    [Export] State initialState = null;

    [ExportSubgroup("Debug")]
    [Export] Label currentStateLabel;

    public State state {get; private set;}


    public override async void _Ready()
    {
        base._Ready();
        if(initialState != null)
            {
                state = initialState;
            }
            else
            {
                state = GetChild<State>(0);
            }

        foreach(State stateNode in GetChildren())
        {
            stateNode.Finished += TransitionToNextState;
            GD.Print(stateNode.Name + " found.");
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
        state =  GetNode<State>(targetStatePath);
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
