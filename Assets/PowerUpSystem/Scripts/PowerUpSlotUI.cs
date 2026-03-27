using UnityEngine;
using UnityEngine.UI;

namespace PowerUpSystem.Scripts
{
    public class PowerUpSlotUI : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _durationText;
        [SerializeField] private GameObject _selectedMarker;
        [SerializeField] private string _emptyLabel = "Empty";

        public void Bind(PowerUpSlotData data)
        {
            if (_nameText != null)
            {
                _nameText.text = data.HasPowerUp ? data.Name : _emptyLabel;
            }

            if (_durationText != null)
            {
                _durationText.text = data.HasPowerUp ? $"{data.Duration:0.0}s" : string.Empty;
            }

            if (_selectedMarker != null)
            {
                _selectedMarker.SetActive(data.IsSelected && data.HasPowerUp);
            }
        }
    }
}
