using UnityEngine;

namespace PowerUpSystem.Scripts
{
   public class ActivePowerUpEffect : MonoBehaviour
   {
      private PowerUp _effect;
      private float _remainingTime;

      public void StartEffect(PlayerForPowerUp player)
      {
         
      }

      public void TickEffect(PlayerForPowerUp player, float deltaTime)
      {
         
      }

      public bool IsExpired()
      {
         return _remainingTime <= 0f;
      }

      public void EndEffect(PlayerForPowerUp player)
      {
         _effect = null;
      }
   
   }
}
