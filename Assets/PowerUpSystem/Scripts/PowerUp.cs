

//2026-03-24    Mahan Poor Hamidian Creation of PowerUp

namespace PowerUpSystem.Scripts
{
    public abstract class PowerUp
    {
        protected string name;
        protected float duration;

        public abstract void ApplyEffects(); // player
        public abstract void RemoveEffects(); // player
        public abstract PowerUp Clone();
    }
}
