using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpSystemFacade : MonoBehaviour
    {
        private static PowerUpSystemFacade _instance;
        public static PowerUpSystemFacade Instance => _instance;

        private PowerUpPool _pickUpPool;
        private PowerUpEvents _powerUpEvents;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            if (_pickUpPool == null)
            {
                _pickUpPool = GetComponent<PowerUpPool>();
            }

            if (_powerUpEvents == null)
            {
                _powerUpEvents = GetComponent<PowerUpEvents>();
            }
        }

        public void CollectPickup(PowerUpPickup pickup, PlayerForPowerUp player)
        {
            if (pickup == null || player == null)
            {
                Debug.LogWarning("[PowerUp] CollectPickup failed: pickup or player is null.");
                return;
            }

            PowerUp prototype = pickup.Prototype;
            if (prototype == null)
            {
                Debug.LogWarning("[PowerUp] Pickup has no prototype.");
                return;
            }

            InventoryManager inventory = player.GetInventoryManager();
            if (inventory == null)
            {
                Debug.LogWarning("[PowerUp] Player has no inventory.");
                return;
            }

            PowerUp cloned = prototype.Clone();
            if (cloned == null)
            {
                Debug.LogWarning("[PowerUp] Prototype clone failed.");
                return;
            }

            bool added = inventory.AddPowerUp(cloned);
            if (!added)
            {
                return;
            }

            _powerUpEvents?.NotifyCollected(cloned);
            _powerUpEvents?.NotifyInventoryChanged();

            if (_pickUpPool != null)
            {
                _pickUpPool.Release(pickup);
            }
            else
            {
                pickup.gameObject.SetActive(false);
            }
        }

        public void ActivateSelected(PlayerForPowerUp player)
        {
            if (player == null)
            {
                Debug.LogWarning("[PowerUp] ActivateSelected failed: player is null.");
                return;
            }

            InventoryManager inventory = player.GetInventoryManager();
            if (inventory == null)
            {
                Debug.LogWarning("[PowerUp] ActivateSelected failed: inventory missing.");
                return;
            }

            PowerUp selectedPowerUp = inventory.GetSelectedPowerUp();
            if (selectedPowerUp == null)
            {
                Debug.Log("[PowerUp] No power up selected.");
                return;
            }

            PlayerPowerUpController controller = player.GetPowerUpController();
            if (controller == null)
            {
                Debug.LogWarning("[PowerUp] ActivateSelected failed: controller missing.");
                return;
            }

            controller.Activate(selectedPowerUp, player);
            inventory.RemoveSelectedPowerUp();
            _powerUpEvents?.NotifyActivated(selectedPowerUp);
            _powerUpEvents?.NotifyInventoryChanged();
        }

        public void UpdateEffects(PlayerForPowerUp player, float deltaTime)
        {
            if (player == null)
            {
                return;
            }

            PlayerPowerUpController controller = player.GetPowerUpController();
            if (controller == null)
            {
                return;
            }

            controller.UpdateEffects(player, deltaTime);
        }

        public void NotifyExpired(PowerUp powerUp)
        {
            _powerUpEvents?.NotifyExpired(powerUp);
        }

        public InventoryManager GetInventory(PlayerForPowerUp player)
        {
            return player?.GetInventoryManager();
        }
    }
}
