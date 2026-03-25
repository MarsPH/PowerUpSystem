using System.Collections.Generic;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpPool : MonoBehaviour
    {
        private Queue<PowerUpPickup> _pickupPool = new Queue<PowerUpPickup>();

        public PowerUpPickup GetPowerUpPickup()
        {
            return _pickupPool.Count > 0 ? _pickupPool.Dequeue() : null;
        }

        public void ReleasePowerUpPickup(PowerUpPickup pickup)
        {
            _pickupPool.Enqueue(pickup);
        }
    }
}
