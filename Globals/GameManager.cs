using Godot;
using System;

public partial class GameManager : Node
{

    [Signal]
    public delegate void OnPlayerDiedEventHandler();

    [Signal]
    public delegate void OnPauseGameEventHandler();

    bool gameIsPaused;

    //All the logic for handling the game

    //Game difficulty affects enemy health multipliers and the rarity and quantity of lootable items
    public static int GAME_DIFFICULTY = 1;

    //Store the current game state

    public override void _EnterTree()
    {
        base._EnterTree();
        OnPlayerDied += RestartAtCheckpoint;
        OnPauseGame += PauseGame;
    }

    public override void _Ready()
    {
        ProcessMode = Node.ProcessModeEnum.Always;

        base._Ready();
    }
    public void LoadScene()
    {
        //Change the current scene to the new one
    }
    public void RestartAtCheckpoint()
    {

    }


    void PauseGame()
    {
        if(GetTree().Paused)
        {
            GetTree().Paused = false;
            GD.Print("Game is unpaused");

        }
        else
        {
            GetTree().Paused = true;
            GD.Print("Game is paused");
        }

        gameIsPaused = GetTree().Paused;

    }   

    void ChangeGameDifficulty(int value)
    {
        //Set the game difficulty to the new value, but make sure it can't go below 1
        GAME_DIFFICULTY = Mathf.Max(value, 1);
    }
}
