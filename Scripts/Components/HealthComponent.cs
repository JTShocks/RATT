using Godot;
using System;

[Tool]
    public partial class HealthComponent : Node2D
    {
        [Signal]
        public delegate void HealthChangedEventHandler(HealthUpdate healthUpdate);
        [Signal]
        public delegate void OnTakeDamageEventHandler();
        [Signal]
        public delegate void DiedEventHandler();

        [Export]
        public float MaxHealth
        {
            get => maxHealth;
            private set{
                maxHealth = value;
                if(CurrentHealth > maxHealth)
                {
                    CurrentHealth = maxHealth;
                }
            }
        }
        [Export]
        private bool suppressDamageFloat;

        public bool HasHealthRemaining => !Mathf.IsEqualApprox(CurrentHealth,0f);

        public float CurrentHealthPercent => MaxHealth > 0 ? currentHealth / MaxHealth :0f;

        public float CurrentHealth
        {
            get => currentHealth;
            private set
            {
                var previousHealth = currentHealth;
                currentHealth = Mathf.Clamp(value, 0, MaxHealth);
                var healthUpdate = new HealthUpdate
                {
                    PreviousHealth = previousHealth,
                    CurrentHealth = currentHealth,
                    MaxHealth = maxHealth,
                    HealthPercent = CurrentHealthPercent,
                    IsHeal = previousHealth <= currentHealth
                };
                EmitSignal(SignalName.HealthChanged, healthUpdate);
                if(!HasHealthRemaining && !hasDied)
                {
                    hasDied = true;
                    EmitSignal(SignalName.Died);
                }

            }
        }

        public bool IsDamaged => CurrentHealth < MaxHealth;

        private float currentHealth;
        private float maxHealth;
        private bool hasDied;
        //This is what will include the ITakeDamage interface
        //When a hitbox recieves a collision, it will send a signal to the TakeDamage function here

        public override void _Ready()
        {
            CallDeferred(nameof(InitializeHealth));
        }

        public void Damage(float damage, bool forceHideDamage = false)
        {
            CurrentHealth -= damage;
            EmitSignal(SignalName.OnTakeDamage);
            //if(!suppressDamageFloat && ! forceHideDamage)
            //{
            //    currentDamageFloat = 
            //}
        }

        public void Heal(float heal)
        {
            Damage(-heal, true);
        }

        public void SetMaxHealth(float health)
        {
            MaxHealth = health;
        }

        private void InitializeHealth()
        {
            CurrentHealth = MaxHealth;
        }


        public partial class HealthUpdate : RefCounted
        {
            public float PreviousHealth;
            public float CurrentHealth;
            public float MaxHealth;
            public float HealthPercent;
            public bool IsHeal;
        }


    }

