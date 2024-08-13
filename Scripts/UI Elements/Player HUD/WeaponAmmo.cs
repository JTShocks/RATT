using Godot;
using System;

public partial class WeaponAmmo : Control
{

	[Export] Label currentAmmo;
	[Export] Label remainingAmmo;

	public void OnUpdateWeaponUI(Weapon weapon)
	{
		currentAmmo.Text = weapon.CurrentAmmo.ToString();
		remainingAmmo.Text = weapon.RemainingAmmo.ToString();
	}
}
