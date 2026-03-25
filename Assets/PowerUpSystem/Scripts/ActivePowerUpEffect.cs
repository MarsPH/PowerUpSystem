namespace PowerUpSystem.Scripts
{
    public class ActivePowerUpEffect
    {
        private readonly PowerUp _effect;
        private float _remainingTime;

        public PowerUp Effect => _effect;
        public float RemainingTime => _remainingTime;

        public ActivePowerUpEffect(PowerUp effect)
        {
            _effect = effect;
            _remainingTime = effect?.Duration ?? 0f;
        }

        public void Start(PlayerForPowerUp player)
        {
            _effect?.ApplyEffects(player);
        }

        public void Tick(PlayerForPowerUp player, float deltaTime)
        {
            if (_effect == null || IsExpired())
            {
                return;
            }

            _remainingTime -= deltaTime < 0f ? 0f : deltaTime;
        }

        public bool IsExpired()
        {
            return _remainingTime <= 0f;
        }

        public void End(PlayerForPowerUp player)
        {
            _effect?.RemoveEffects(player);
        }
    }
}
