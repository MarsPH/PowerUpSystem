using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class SpeedBoostPowerUp : PowerUp
    {
        public SpeedBoostPowerUp() : this("Speed Boost", 5f)
        {
        }

        public SpeedBoostPowerUp(string powerUpName, float powerUpDuration)
        {
            name = powerUpName;
            duration = powerUpDuration;
        }

        public override void ApplyEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} applied to {player?.name ?? "Unknown Player"}");
        }

        public override void RemoveEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} removed from {player?.name ?? "Unknown Player"}");
        }

        public override PowerUp Clone()
        {
            return new SpeedBoostPowerUp(name, Duration);
        }
    }
}