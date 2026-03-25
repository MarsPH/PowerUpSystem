using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PlayerForPowerUp : MonoBehaviour
    {
        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private PlayerPowerUpController _playerPowerUpController;
        [SerializeField] private KeyCode _activateKey = KeyCode.E;

        private void Awake()
        {
            if (_inventoryManager == null)
            {
                _inventoryManager = GetComponent<InventoryManager>();
            }

            if (_playerPowerUpController == null)
            {
                _playerPowerUpController = GetComponent<PlayerPowerUpController>();
            }
        }

        public InventoryManager GetInventoryManager() => _inventoryManager;

        public PlayerPowerUpController GetPowerUpController()
        {
            return _playerPowerUpController;
        }
        
        private void Update()
        {
            if (PowerUpSystemFacade.Instance != null)
            {
                PowerUpSystemFacade.Instance.UpdateEffects(this, Time.deltaTime);
            }

            if (Input.GetKeyDown(_activateKey))
            {
                if (PowerUpSystemFacade.Instance != null)
                {
                    PowerUpSystemFacade.Instance.ActivateSelected(this);
                }
            }
        }
    }
}
