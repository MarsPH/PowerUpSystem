

//2026-03-24    Mahan Poor Hamidian Creation of PowerUp

namespace PowerUpSystem.Scripts
{
    public abstract class PowerUp
    {
        protected string name;
        protected float duration;

        public abstract void ApplyEffects(PlayerForPowerUp player); // player
        public abstract void RemoveEffects(PlayerForPowerUp player); // player
        public abstract PowerUp Clone();
    }
}
