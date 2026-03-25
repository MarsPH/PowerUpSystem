using System.Collections.Generic;
using PowerUpSystem.Scripts;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<PowerUp> _powerUps;
    private int _capacity;
    private int _selectedIndex;

    public bool AddPowerUp(PowerUp powerUp)
    {
        return true;
    }

    public PowerUp RemoveSelectedPowerUp(PowerUp powerUp)
    {
        _powerUps.Remove(powerUp);
        return powerUp;
    }

    public PowerUp GetSelectedPowerUp()
    {
        return _powerUps[_selectedIndex];
    }

    public void SelectNextPowerUp()
    {
        _selectedIndex = (_selectedIndex + 1) % _powerUps.Count;
    }

    public void SelectPreviousPowerUp()
    {
        _selectedIndex = (_selectedIndex - 1 + _powerUps.Count) % _powerUps.Count;
    }

    public bool IsFull()
    {
        return _powerUps.Count == _capacity;
    }
    
}
