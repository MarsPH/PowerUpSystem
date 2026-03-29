using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PowerUpSystem.Scripts
{
    public class PowerUpSlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _durationText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private GameObject _selectedMarker;
        [SerializeField] private string _emptyLabel = "Empty";

        public void Bind(PowerUpSlotData data)
        {
            Bind(data, null);
        }

        public void Bind(PowerUpSlotData data, Sprite icon)
        {
            if (_nameText != null)
            {
                _nameText.text = data.HasPowerUp ? data.Name : _emptyLabel;
            }

            if (_durationText != null)
            {
                _durationText.text = data.HasPowerUp ? $"{data.Duration:0.0}s" : string.Empty;
            }

            if (_iconImage != null)
            {
                bool show = data.HasPowerUp && icon != null;
                _iconImage.enabled = show;
                if (show)
                {
                    _iconImage.sprite = icon;
                }
            }

            if (_selectedMarker != null)
            {
                _selectedMarker.SetActive(data.IsSelected && data.HasPowerUp);
            }
        }
    }
}
