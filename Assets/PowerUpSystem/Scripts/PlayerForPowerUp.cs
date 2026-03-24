using UnityEngine;

namespace PowerUpSystem.Scripts
{
    public class PlayerForPowerUp : MonoBehaviour
    {
        private PlayerPowerUpController _playerPowerUpController;

        public PlayerPowerUpController GetPowerUpController()
        {
            return _playerPowerUpController;
        }
}
}
