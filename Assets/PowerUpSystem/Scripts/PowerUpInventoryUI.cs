using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpInventoryUI : MonoBehaviour
    {
        [SerializeField] private PlayerForPowerUp _player;
        [SerializeField] private PowerUpSlotUI[] _slots;

        private void Awake()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerForPowerUp>();
            }
        }

        private void Update()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (_slots == null || _slots.Length == 0)
            {
                return;
            }

            InventoryManager inventory = _player != null ? _player.GetInventoryManager() : null;
            if (inventory == null)
            {
                for (int i = 0; i < _slots.Length; i++)
                {
                    if (_slots[i] != null)
                    {
                        _slots[i].Bind(new PowerUpSlotData(false, string.Empty, 0f, false));
                    }
                }
                return;
            }

            PowerUpSlotData[] data = inventory.GetSlotsForUI(true);
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null)
                {
                    continue;
                }

                PowerUpSlotData slotData = i < data.Length
                    ? data[i]
                    : new PowerUpSlotData(false, string.Empty, 0f, false);
                _slots[i].Bind(slotData);
            }
        }
    }
}
