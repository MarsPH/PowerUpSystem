using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PlayerForPowerUp : MonoBehaviour
    {
        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private PlayerPowerUpController _playerPowerUpController;
        [SerializeField] private KeyCode _activateKey = KeyCode.E;
        [Header("Inventory selection (highlights one slot, then press Activate)")]
        [SerializeField] private KeyCode _cycleNextKey = KeyCode.RightBracket;
        [SerializeField] private KeyCode _cyclePreviousKey = KeyCode.LeftBracket;

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

            InventoryManager inventory = _inventoryManager;
            if (inventory != null)
            {
                if (Input.GetKeyDown(_cycleNextKey))
                {
                    inventory.SelectNextPowerUp();
                }

                if (Input.GetKeyDown(_cyclePreviousKey))
                {
                    inventory.SelectPreviousPowerUp();
                }
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
