using PowerUpSystem.Scripts;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpEvents : MonoBehaviour
    {
        public void NotifyCollected(PowerUp powerUp)
        {
            Debug.Log($"[PowerUpEvents] Collected: {powerUp?.Name ?? "Unknown"}");
        }

        public void NotifyPowerUp(PowerUp powerUp)
        {
            NotifyCollected(powerUp);
        }

        public void NotifyActivated(PowerUp powerUp)
        {
            Debug.Log($"[PowerUpEvents] Activated: {powerUp?.Name ?? "Unknown"}");
        }

        public void NotifyExpired(PowerUp powerUp)
        {
            Debug.Log($"[PowerUpEvents] Expired: {powerUp?.Name ?? "Unknown"}");
        }

        public void NotifyInventoryChanged()
        {
            Debug.Log("[PowerUpEvents] Inventory changed.");
        }
    }
}
