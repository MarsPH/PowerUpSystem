namespace PowerUpSystem.Scripts
{
    public class PowerUpDecorator : PowerUp
    {
        private readonly PowerUp powerUpWrapped;

        public PowerUpDecorator(PowerUp wrapped)
        {
            powerUpWrapped = wrapped;
            if (wrapped != null)
            {
                name = wrapped.Name;
                duration = wrapped.Duration;
            }
        }

        public override void ApplyEffects(PlayerForPowerUp player)
        {
            powerUpWrapped?.ApplyEffects(player);
        }

        public override void RemoveEffects(PlayerForPowerUp player)
        {
            powerUpWrapped?.RemoveEffects(player);
        }

        public override PowerUp Clone()
        {
            return new PowerUpDecorator(powerUpWrapped?.Clone());
        }

        public override PowerUpType? GetUiType() => powerUpWrapped?.GetUiType();
    
    }
}
