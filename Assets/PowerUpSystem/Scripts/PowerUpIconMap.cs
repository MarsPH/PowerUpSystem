using UnityEngine;

namespace PowerUpSystem.Scripts
{
    /// <summary>
    /// Maps <see cref="PowerUpType"/> to UI sprites. Array index must match enum:
    /// 0 = SpeedBoost, 1 = Phasing, 2 = DoubleJump, 3 = AntiBounce.
    /// </summary>
    public class PowerUpIconMap : MonoBehaviour
    {
        [SerializeField] private Sprite[] _iconsByType;

        public Sprite GetIcon(PowerUpType? type)
        {
            if (type == null || _iconsByType == null || _iconsByType.Length == 0)
            {
                return null;
            }

            int index = (int)type.Value;
            if (index < 0 || index >= _iconsByType.Length)
            {
                return null;
            }

            return _iconsByType[index];
        }
    }
}
