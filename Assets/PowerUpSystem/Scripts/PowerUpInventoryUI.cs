using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpInventoryUI : MonoBehaviour
    {
        [SerializeField] private PlayerForPowerUp _player;
        [SerializeField] private PowerUpSlotUI[] _slots;
        [SerializeField] private PowerUpIconMap _iconMap;

        private void Awake()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerForPowerUp>();
            }

            if (_iconMap == null)
            {
                _iconMap = GetComponent<PowerUpIconMap>();
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
                        _slots[i].Bind(new PowerUpSlotData(false, string.Empty, 0f, false, null), null);
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
                    : new PowerUpSlotData(false, string.Empty, 0f, false, null);
                Sprite icon = _iconMap != null ? _iconMap.GetIcon(slotData.UiType) : null;
                _slots[i].Bind(slotData, icon);
            }
        }
    }
}
