using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class DoubleJumpPowerUp : PowerUp
    {
        public DoubleJumpPowerUp() : this("Double Jump", 8f)
        {
        }

        public DoubleJumpPowerUp(string powerUpName, float powerUpDuration)
        {
            name = powerUpName;
            duration = powerUpDuration;
        }

        public override void ApplyEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} applied to {player?.name ?? "Unknown Player"} (debug extra jump on).");
        }

        public override void RemoveEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} removed from {player?.name ?? "Unknown Player"} (debug extra jump off).");
        }

        public override PowerUp Clone()
        {
            return new DoubleJumpPowerUp(name, Duration);
        }
    }
}
