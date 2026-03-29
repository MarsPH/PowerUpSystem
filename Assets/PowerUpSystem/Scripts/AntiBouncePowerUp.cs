using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class AntiBouncePowerUp : PowerUp
    {
        public AntiBouncePowerUp() : this("Anti Bounce", 6f)
        {
        }

        public AntiBouncePowerUp(string powerUpName, float powerUpDuration)
        {
            name = powerUpName;
            duration = powerUpDuration;
        }

        public override PowerUpType? GetUiType() => PowerUpType.AntiBounce;

        public override void ApplyEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} applied to {player?.name ?? "Unknown Player"} (debug bounce reduction on).");
        }

        public override void RemoveEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} removed from {player?.name ?? "Unknown Player"} (debug bounce reduction off).");
        }

        public override PowerUp Clone()
        {
            return new AntiBouncePowerUp(name, Duration);
        }
    }
}
