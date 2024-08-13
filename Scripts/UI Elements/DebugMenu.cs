using Godot;
using System;

public partial class DebugMenu : Control
{
    [Export] PlayerController player;


    [ExportCategory("Location Markers")]
    [Export] Label playerPosition;
    [Export] Label currentMap;


    public override void _Ready()
    {
        base._Ready();

        currentMap.Text = GetTree().CurrentScene.Name;
    }

    public override void _Process(double delta)
    {

        Vector2 position = new Vector2((int)player.GlobalPosition.X, (int)player.GlobalPosition.Y);
        playerPosition.Text = "Position: " + position.ToString();
        base._Process(delta);
    }
}
