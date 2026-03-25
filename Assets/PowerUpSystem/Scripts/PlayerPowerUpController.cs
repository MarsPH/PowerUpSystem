using System.Collections.Generic;
using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PlayerPowerUpController : MonoBehaviour
    {
        private List<ActivePowerUpEffect> _activeEffects;

        private void Awake()
        {
            _activeEffects ??= new List<ActivePowerUpEffect>();
        }

        public void Activate(PowerUp powerUp, PlayerForPowerUp player)
        {
            if (powerUp == null)
            {
                Debug.LogWarning("[PowerUp] Cannot activate null power up.");
                return;
            }

            if (player == null)
            {
                Debug.LogWarning("[PowerUp] Cannot activate power up for null player.");
                return;
            }

            ActivePowerUpEffect activeEffect = new ActivePowerUpEffect(powerUp);
            activeEffect.Start(player);
            _activeEffects.Add(activeEffect);
        }

        public void UpdateEffects(PlayerForPowerUp player, float deltaTime)
        {
            if (_activeEffects == null || _activeEffects.Count == 0 || player == null)
            {
                return;
            }

            for (int i = 0; i < _activeEffects.Count; i++)
            {
                _activeEffects[i].Tick(player, deltaTime);
            }

            RemoveExpired(player);
        }

        public void RemoveExpired(PlayerForPowerUp player)
        {
            if (_activeEffects == null || _activeEffects.Count == 0 || player == null)
            {
                return;
            }

            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                ActivePowerUpEffect effect = _activeEffects[i];
                if (!effect.IsExpired())
                {
                    continue;
                }

                effect.End(player);
                PowerUpSystemFacade.Instance?.NotifyExpired(effect.Effect);
                _activeEffects.RemoveAt(i);
            }
        }
    }
}
