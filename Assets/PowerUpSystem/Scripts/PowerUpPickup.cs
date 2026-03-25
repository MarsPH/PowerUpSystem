using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpPickup : MonoBehaviour
    {
        private PowerUp _prototype;
        
        public PowerUp Prototype => _prototype;
        

        private void Awake()
        {
            _prototype = new SpeedBoostPowerUp();
        }
        
        // Called when the player touches this pickup.
        // If this pickup has a valid PowerUp prototype, it creates a clone of that PowerUp,
        // tries to add it to the player's inventory, and disables the pickup if the add succeeds.
        public void OnPlayerTouch(PlayerForPowerUp player)
        {
            if (_prototype == null) return;
            PowerUp clonedPowerUp = _prototype.Clone();
            if (player.GetInventoryManager().AddPowerUp(clonedPowerUp))
            {
                gameObject.SetActive(false);
            }
        }

        public void ResetWithPrototype(PowerUp prototype)
        {
            _prototype = prototype;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            PlayerForPowerUp player = other.GetComponent<PlayerForPowerUp>();
            if (player != null)
            {
                OnPlayerTouch(player);
            }
        }
    }
}
