using Godot;
using System;

public partial class Interactable : Area2D
{
    //This is a base class that interactable components will inherit from, since all of them need these features.

    //In this case, a signal would be better, since these are only going to be linked within a given scene
    [Signal]
    public delegate void OnInteractEventHandler();

    [ExportGroup("Interactable Values")]

    [Export] string interactText = "Activate";
    [Export] Label interactPrompt;
    [Export] Sprite2D sprite;
    [Export] public float interactDuration; // how long the player needs to interact with the interactable feature for it to complete.

    
    protected bool IsActive;

    //When determining if a player should interact with something, the prompt should ONLY be on the one highlighted

    //Interactable defines the ZONE of a given interactable AND sends out an event on interacted

    public override void _Ready()
    {
        interactPrompt.Text = "<key>\n to " + interactText;
        interactPrompt.Visible = false;
        base._Ready();
        var actions = InputMap.ActionGetEvents("Interact")[0];
        if(actions is InputEventKey key)
        {
            interactPrompt.Text = interactPrompt.Text.Replace("<key>",key.PhysicalKeycode.ToString());
        }
       
    }

    public void Interact()
    {
        EmitSignal(SignalName.OnInteract);
    }

    public void OnSelected()
    {
        //Activate the prompt
        interactPrompt.Visible = true;
        //Create a highlight around the object

    }
    public void OnDeselected()
    {
        interactPrompt.Visible = false;
        //remove the highlight around the sprite
    }

}
