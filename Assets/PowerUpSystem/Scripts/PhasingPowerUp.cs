using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PhasingPowerUp : PowerUp
    {
        public PhasingPowerUp() : this("Phasing", 5f)
        {
        }

        public PhasingPowerUp(string powerUpName, float powerUpDuration)
        {
            name = powerUpName;
            duration = powerUpDuration;
        }

        public override PowerUpType? GetUiType() => PowerUpType.Phasing;

        public override void ApplyEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} applied to {player?.name ?? "Unknown Player"} (debug phasing on).");
        }

        public override void RemoveEffects(PlayerForPowerUp player)
        {
            Debug.Log($"[PowerUp] {Name} removed from {player?.name ?? "Unknown Player"} (debug phasing off).");
        }

        public override PowerUp Clone()
        {
            return new PhasingPowerUp(name, Duration);
        }
    }
}
