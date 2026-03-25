using System.Collections.Generic;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class InventoryManager : MonoBehaviour
    {
        private List<PowerUp> _powerUps = new List<PowerUp>();
        [SerializeField] private int _capacity = 3;
        private int _selectedIndex;

        public bool AddPowerUp(PowerUp powerUp)
        {
            if (IsFull())
            {
                Debug.Log("Cannot add power up because inventory is full");
                return false;
            }
        
            _powerUps.Add(powerUp);
            Debug.Log("Adding power up " + powerUp);
            return true;

        }

        public PowerUp RemoveSelectedPowerUp(PowerUp powerUp)
        {
            if (_powerUps.Count == 0) return null;

            PowerUp selected = _powerUps[_selectedIndex];
            _powerUps.RemoveAt(_selectedIndex);

            if (_selectedIndex >= _powerUps.Count && _powerUps.Count > 0)
                _selectedIndex = 0;

            return selected;
        }

        public PowerUp GetSelectedPowerUp()
        {
            if(_powerUps.Count == 0) return null;
            return _powerUps[_selectedIndex];
        }

        public void SelectNextPowerUp()
        {
            if (_powerUps.Count == 0) return;
            _selectedIndex = (_selectedIndex + 1) % _powerUps.Count;
        }

        public void SelectPreviousPowerUp()
        {
            if (_powerUps.Count == 0) return;
            _selectedIndex = (_selectedIndex - 1 + _powerUps.Count) % _powerUps.Count;
        }

        public bool IsFull()
        {
            if (_powerUps.Count < _capacity)
            {
                Debug.Log(" Inventory Not Full");
                return false;
            }

            Debug.Log(" Inventory Full");
            return true;
        }
    
    }
}
