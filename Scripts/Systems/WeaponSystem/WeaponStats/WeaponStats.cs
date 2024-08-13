using Godot;
using System;

[GlobalClass]
public partial class WeaponStats : Resource
{

    [ExportCategory("Generic Stats")]
    [Export] int _baseDamage = 1; 
    [Export] float _baseRateOfFire = 1; //How many times it can fire per second
    [Export] float _weaponWeight = 1; //Alters multiple stats
    [Export] float _weaponShakeForce = 0.0f;
    [Export] float _screenShakeDecay = 0.8f;

    [ExportGroup("Gun Stats")]
    [Export] int _maxAmmoReserve = 1;
    [Export] int _baseClipSize = 1; //How many shots per clip of the weapon
    [Export] float _baseSpread = 1f; //+- variance in bullet path in degrees from the initial fire angle
    [Export] float _baseReloadSpeed = 2; // Reload speed in seconds

    [ExportGroup("Charge Stats")]
    [Export] float _chargeRate = 10f; //How fast the weapon charges per second

    
    



    public int BaseDamage => _baseDamage;
    public float BaseRateOfFire => _baseRateOfFire;
    public int MaxAmmoReserve => _maxAmmoReserve;
    public int BaseClipSize => _baseClipSize;
    public float BaseSpread => _baseSpread;
    public float BaseReloadSpeed => _baseReloadSpeed;

    public float WeaponShakeForce => _weaponShakeForce;
    public float ScreenShakeDecay => _screenShakeDecay;

    public float ChargeRate => _chargeRate;


}
