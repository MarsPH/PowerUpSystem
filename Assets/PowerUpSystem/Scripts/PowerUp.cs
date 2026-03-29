

//2026-03-24    Mahan Poor Hamidian Creation of PowerUp

namespace PowerUpSystem.Scripts
{
    public abstract class PowerUp
    {
        protected string name;
        protected float duration;
        
        public string Name => string.IsNullOrWhiteSpace(name) ? GetType().Name : name;
        public float Duration => duration < 0f ? 0f : duration;

        /// <summary>For UI icons / radial slots. Null if this power-up has no mapped enum.</summary>
        public virtual PowerUpType? GetUiType() => null;

        public abstract void ApplyEffects(PlayerForPowerUp player); // player
        public abstract void RemoveEffects(PlayerForPowerUp player); // player
        public abstract PowerUp Clone();
    }
}
