using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class SpeedBoostPowerUp : PowerUp
    {
        public SpeedBoostPowerUp()
        {
            name = "Speed Boost";
            duration = 5f;
        }

        public override void ApplyEffects(PlayerForPowerUp player)
        {
            Debug.Log("SpeedBoost applied to player");
        }

        public override void RemoveEffects(PlayerForPowerUp player)
        {
            Debug.Log("SpeedBoost removed from player");
        }

        public override PowerUp Clone()
        {
            return new SpeedBoostPowerUp();
        }
    }
}