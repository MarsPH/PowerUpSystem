using System.Collections.Generic;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PlayerPowerUpController : MonoBehaviour
    {
        private List<ActivePowerUpEffect> _activeEffects;

        public void Activate(PowerUp powerUp, PlayerForPowerUp player)
        {
            powerUp.ApplyEffects(player);
        }

        public void UpdateEffects(PlayerForPowerUp player, float deltaTime)
        {
            
        }

        public void RemoveExpired(PlayerForPowerUp player)
        {
            
        }
    }
}
