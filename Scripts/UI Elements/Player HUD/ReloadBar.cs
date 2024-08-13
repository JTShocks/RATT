using Godot;
using System;



public partial class ReloadBar : ProgressBar
{

    Weapon currentWeapon;
    public override void _Ready()
    {
        base._Ready();

        Visible = false;
        //Activate the reload bar
        //Have it start progressing
    }

    public override void _Process(double delta)
    {

        if(Visible)
        {
            //If the bar is visible, update it

            if(currentWeapon.weaponType != Weapon.WeaponType.Melee)
            {
                Value = ((currentWeapon.ReloadTimer.WaitTime - currentWeapon.ReloadTimer.TimeLeft) / currentWeapon.ReloadTimer.WaitTime) * 100;
            }
            else
            {
                Value = currentWeapon.CurrentCharge;
            }
            

        }
        base._Process(delta);
    }
    public void Activate(Weapon weapon)
    {
        currentWeapon = weapon;
        Visible = true;
    }

    public void Deactivate()
    {
        Visible = false;
    }
}
