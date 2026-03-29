using UnityEngine;

namespace PowerUpSystem.Scripts
{
    /// <summary>
    /// Binds inventory data to a circular / radial HUD: four wedge slots around a center.
    /// Slot order (clockwise from top, like a typical radial menu):
    /// [0] Top, [1] Right, [2] Bottom, [3] Left.
    /// Position the slot roots manually in the Canvas (same as your reference art).
    /// </summary>
    public class PowerUpRadialInventoryUI : MonoBehaviour
    {
        [SerializeField] private PlayerForPowerUp _player;
        [Tooltip("Order: Top, Right, Bottom, Left")]
        [SerializeField] private PowerUpSlotUI[] _slots;
        [SerializeField] private PowerUpIconMap _iconMap;
        [Header("Procedural ring (optional)")]
        [Tooltip("Draws the donut, hub, and selection highlight. Place as first child under this HUD so icons render on top.")]
        [SerializeField] private RadialRingSegmentsGraphic _ringGraphic;

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

            if (_ringGraphic == null)
            {
                _ringGraphic = GetComponentInChildren<RadialRingSegmentsGraphic>(true);
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
                ClearSlots();
                return;
            }

            SyncRingSelection(inventory);

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

        private void ClearSlots()
        {
            if (_ringGraphic != null)
            {
                _ringGraphic.SelectedSegmentIndex = -1;
            }

            PowerUpSlotData empty = new PowerUpSlotData(false, string.Empty, 0f, false, null);
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] != null)
                {
                    _slots[i].Bind(empty, null);
                }
            }
        }

        private void SyncRingSelection(InventoryManager inventory)
        {
            if (_ringGraphic == null)
            {
                return;
            }

            if (inventory.Count == 0 || inventory.SelectedIndex < 0)
            {
                _ringGraphic.SelectedSegmentIndex = -1;
                return;
            }

            _ringGraphic.SelectedSegmentIndex = inventory.SelectedIndex;
        }
    }
}
