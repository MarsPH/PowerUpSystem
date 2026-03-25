using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpPickup : MonoBehaviour
    {
        [Header("PowerUp Setup")]
        [SerializeField] private PowerUpType _powerUpType = PowerUpType.SpeedBoost;
        [SerializeField] private string _customName = "";
        [SerializeField] private float _durationOverride = -1f;

        private PowerUp _prototype;

        public PowerUp Prototype => _prototype;

        private void Awake()
        {
            BuildPrototypeIfMissing();
        }

        public void OnPlayerTouch(PlayerForPowerUp player)
        {
            if (player == null)
            {
                return;
            }

            if (PowerUpSystemFacade.Instance != null)
            {
                PowerUpSystemFacade.Instance.CollectPickup(this, player);
                return;
            }

            if (_prototype == null)
            {
                Debug.LogWarning("[PowerUp] Pickup has no prototype.");
                return;
            }

            InventoryManager inventory = player.GetInventoryManager();
            if (inventory == null)
            {
                Debug.LogWarning("[PowerUp] Player inventory is missing.");
                return;
            }

            PowerUp clonedPowerUp = _prototype.Clone();
            if (inventory.AddPowerUp(clonedPowerUp))
            {
                gameObject.SetActive(false);
            }
        }

        public void ResetWithPrototype(PowerUp prototype)
        {
            _prototype = prototype ?? PowerUpFactory.Create(_powerUpType, _customName, _durationOverride);
            gameObject.SetActive(true);
        }

        private void BuildPrototypeIfMissing()
        {
            if (_prototype != null)
            {
                return;
            }

            _prototype = PowerUpFactory.Create(_powerUpType, _customName, _durationOverride);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerForPowerUp player = other.GetComponent<PlayerForPowerUp>();
            if (player != null)
            {
                OnPlayerTouch(player);
            }
        }
    }
}
