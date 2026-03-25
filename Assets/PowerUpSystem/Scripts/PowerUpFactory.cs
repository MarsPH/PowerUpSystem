using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public static class PowerUpFactory
    {
        public static PowerUp Create(PowerUpType type, string customName = null, float durationOverride = -1f)
        {
            PowerUp basePowerUp = type switch
            {
                PowerUpType.SpeedBoost => new SpeedBoostPowerUp(),
                PowerUpType.Phasing => new PhasingPowerUp(),
                PowerUpType.DoubleJump => new DoubleJumpPowerUp(),
                PowerUpType.AntiBounce => new AntiBouncePowerUp(),
                _ => new SpeedBoostPowerUp()
            };

            string finalName = string.IsNullOrWhiteSpace(customName) ? basePowerUp.Name : customName;
            float finalDuration = durationOverride > 0f ? durationOverride : basePowerUp.Duration;

            switch (type)
            {
                case PowerUpType.SpeedBoost:
                    return new SpeedBoostPowerUp(finalName, finalDuration);
                case PowerUpType.Phasing:
                    return new PhasingPowerUp(finalName, finalDuration);
                case PowerUpType.DoubleJump:
                    return new DoubleJumpPowerUp(finalName, finalDuration);
                case PowerUpType.AntiBounce:
                    return new AntiBouncePowerUp(finalName, finalDuration);
                default:
                    Debug.LogWarning("[PowerUp] Unknown type, defaulting to SpeedBoost.");
                    return new SpeedBoostPowerUp(finalName, finalDuration);
            }
        }
    }
}
