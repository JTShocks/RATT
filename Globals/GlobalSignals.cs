using Godot;
using System;

public partial class GlobalSignals : Node
{
    
    [Signal]
    public delegate void TriggerScreenShakeEventHandler(float force, float decay);
}
