using System.Collections.Generic;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class InventoryManager : MonoBehaviour
    {
        private List<PowerUp> _powerUps;
        [SerializeField] private int _capacity = 3;
        private int _selectedIndex = -1;

        public int Count => _powerUps?.Count ?? 0;
        public int Capacity => _capacity;

        private void Awake()
        {
            _powerUps ??= new List<PowerUp>();
            if (_capacity < 1)
            {
                _capacity = 1;
            }
        }

        public bool AddPowerUp(PowerUp powerUp)
        {
            if (powerUp == null)
            {
                Debug.LogWarning("[PowerUp] Cannot add null power up.");
                return false;
            }

            if (IsFull())
            {
                Debug.Log("[PowerUp] Cannot add power up because inventory is full.");
                return false;
            }

            _powerUps.Add(powerUp);
            if (_selectedIndex < 0)
            {
                _selectedIndex = 0;
            }

            Debug.Log($"[PowerUp] Added {powerUp.Name}.");
            return true;
        }

        public PowerUp RemoveSelectedPowerUp()
        {
            if (_powerUps == null || _powerUps.Count == 0 || _selectedIndex < 0 || _selectedIndex >= _powerUps.Count)
            {
                return null;
            }

            PowerUp selected = _powerUps[_selectedIndex];
            _powerUps.RemoveAt(_selectedIndex);

            if (_powerUps.Count == 0)
            {
                _selectedIndex = -1;
            }
            else if (_selectedIndex >= _powerUps.Count)
            {
                _selectedIndex = _powerUps.Count - 1;
            }

            return selected;
        }

        public PowerUp GetSelectedPowerUp()
        {
            if (_powerUps == null || _powerUps.Count == 0)
            {
                return null;
            }

            if (_selectedIndex < 0 || _selectedIndex >= _powerUps.Count)
            {
                _selectedIndex = 0;
            }
            
            return _powerUps[_selectedIndex];
        }

        public void SelectNextPowerUp()
        {
            if (_powerUps == null || _powerUps.Count == 0)
            {
                _selectedIndex = -1;
                return;
            }

            if (_selectedIndex < 0)
            {
                _selectedIndex = 0;
                return;
            }
            
            _selectedIndex = (_selectedIndex + 1) % _powerUps.Count;
        }

        public void SelectPreviousPowerUp()
        {
            if (_powerUps == null || _powerUps.Count == 0)
            {
                _selectedIndex = -1;
                return;
            }

            if (_selectedIndex < 0)
            {
                _selectedIndex = 0;
                return;
            }

            _selectedIndex = (_selectedIndex - 1 + _powerUps.Count) % _powerUps.Count;
        }

        public bool IsFull()
        {
            return _powerUps != null && _powerUps.Count >= _capacity;
        }

    }
}
