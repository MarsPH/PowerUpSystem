using System.Collections.Generic;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpPool : MonoBehaviour
    {
        [SerializeField] private PowerUpPickup _pickupPrefab;
        [SerializeField] private Transform _poolParent;
        private readonly Queue<PowerUpPickup> _pickupPool = new Queue<PowerUpPickup>();

        public PowerUpPickup Get()
        {
            while (_pickupPool.Count > 0)
            {
                PowerUpPickup pickup = _pickupPool.Dequeue();
                if (pickup == null)
                {
                    continue;
                }

                pickup.gameObject.SetActive(true);
                return pickup;
            }

            if (_pickupPrefab != null)
            {
                return Instantiate(_pickupPrefab, _poolParent);
            }

            Debug.LogWarning("[PowerUp] Pool is empty and no prefab is configured.");
            return null;
        }

        public void Release(PowerUpPickup pickup)
        {
            if (pickup == null)
            {
                return;
            }

            pickup.gameObject.SetActive(false);
            if (_poolParent != null)
            {
                pickup.transform.SetParent(_poolParent);
            }

            _pickupPool.Enqueue(pickup);
        }

        public PowerUpPickup GetPowerUpPickup()
        {
            return Get();
        }

        public void ReleasePowerUpPickup(PowerUpPickup pickup)
        {
            Release(pickup);
        }
    }
}
