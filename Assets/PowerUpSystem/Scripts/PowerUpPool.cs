using System.Collections.Generic;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PowerUpPool : MonoBehaviour
    {
        [Tooltip("Only used when Get() runs and the queue is empty — e.g. runtime spawning. Not needed if pickups only come from scene objects recycled via Release().")]
        [SerializeField] private PowerUpPickup _pickupPrefab;
        [Tooltip("Inactive pickups are parented here for a tidy hierarchy. If unset, defaults to this object.")]
        [SerializeField] private Transform _poolParent;
        private readonly Queue<PowerUpPickup> _pickupPool = new Queue<PowerUpPickup>();

        private void Awake()
        {
            if (_poolParent == null)
            {
                _poolParent = transform;
            }
        }

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
                return Instantiate(_pickupPrefab, _poolParent != null ? _poolParent : transform);
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
            pickup.transform.SetParent(_poolParent != null ? _poolParent : transform);

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
