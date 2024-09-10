using Godot;
using System;
using System.Net.NetworkInformation;

public partial class Interactor : Area2D
{

    [Export] InputComponent playerInput;

    //When an interactable object enters the range of the interactor
    //Show the display prompt to interact

    //Each interactable has an "interactTime", which is the duration for how long it takes to complete the interaction and how long the player needs to hold the button.


    //Needs to check if the interaction actually passed
    //What if a play tries interacting with a locked door without the key?
    //What if a switch for an elevator needs power first and then becomes active?


    //All the interactor needs to do is know there is an interactable object in range and, when the player inputs it, trigger the interaction

    public static Action BeginInteract; //Send out an event for when the player starts to interact
    //The player cannot keep reloading while interacting, so the reloading timer should pause when the player starts to interact something.

    Timer interactTimer;

    Interactable focusTarget = null;


    public override void _Ready()
    {
        base._Ready();

        //Connect up to the signals
        //Create the timer for the interact

        AreaEntered += CheckCurrentFocus;
        AreaExited += RemoveFocus;

    }



    void OnInteract()
    {
        BeginInteract.Invoke();
        //focusTarget.Interact();

    }
    void CheckCurrentFocus(Node2D node)
    {
        if(node is Interactable interactable)
        {
            if(focusTarget == null)
            {
                AssignNewFocus(interactable);
                return;
            }
            //Determine the interactable to highlight based on it's distance to the interactor/player

            float newDistanceToPlayer = this.GlobalPosition.DistanceTo(interactable.GlobalPosition);
            float currentDistanceToPlayer = this.GlobalPosition.DistanceTo(focusTarget.GlobalPosition);

            if(newDistanceToPlayer <= currentDistanceToPlayer)
            {
                DeselectFocus(focusTarget);
                AssignNewFocus(interactable);
            }
        }
    }

    void RemoveFocus(Node2D node)
    {
        if(node is Interactable interactable)
        {
            if(interactable == focusTarget)
            {
                DeselectFocus(focusTarget);
                focusTarget = null;
            }
        }
    }


    void AssignNewFocus(Interactable newFocus)
    {
        focusTarget = newFocus;
        SelectFocus(newFocus);

    }

    void DeselectFocus(Interactable focus)
    {
        focus.OnDeselected();
    }
    void SelectFocus(Interactable focus)
    {
        focus.OnSelected();
    }


}
