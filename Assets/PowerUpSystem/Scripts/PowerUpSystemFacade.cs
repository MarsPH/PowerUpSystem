using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpSystemFacade : MonoBehaviour
    {
        //Singleton Initiliazing
        private static PowerUpSystemFacade _instance;
        public static PowerUpSystemFacade Instance => _instance;
        
        
        private InventoryManager _inventoryManager;
        private PowerUpPool _pickUpPool;
        private PowerUpEvents _powerUpEvents;
        
        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            
            _inventoryManager = GetComponent<InventoryManager>();
            _pickUpPool = GetComponent<PowerUpPool>();
            _powerUpEvents = GetComponent<PowerUpEvents>();
        }

        public void CollectPickup(PowerUpPickup pickup, PlayerForPowerUp player)
        {
            
        }

        public void ActivateSelected(PlayerForPowerUp player)
        {
            InventoryManager inventory = player.GetInventoryManager();
            PowerUp selectedPowerUp = inventory.GetSelectedPowerUp();
            if (selectedPowerUp == null)
            {
                Debug.Log("No PowerUp Selected");
                return;
            }
            player.GetPowerUpController().Activate(selectedPowerUp, player);
            
            inventory.RemoveSelectedPowerUp();
        }

        public void UpdateEffects(PlayerForPowerUp player, float deltaTime)
        {
            
        }

        public InventoryManager GetInventory()
        {
            return _inventoryManager;
        }
        
    }
}
