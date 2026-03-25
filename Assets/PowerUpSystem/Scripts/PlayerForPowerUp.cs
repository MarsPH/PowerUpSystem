using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PlayerForPowerUp : MonoBehaviour
    {
        [SerializeField] private InventoryManager _inventoryManager;
        public InventoryManager GetInventoryManager() => _inventoryManager;
        
        [SerializeField] private PlayerPowerUpController _playerPowerUpController;

        public PlayerPowerUpController GetPowerUpController()
        {
            return _playerPowerUpController;
        }
}
}
