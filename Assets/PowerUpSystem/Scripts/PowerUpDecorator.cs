namespace PowerUpSystem.Scripts
{
    public class PowerUpDecorator : PowerUp
    {
        private PowerUp powerUpWrapped;

        public override void ApplyEffects(PlayerForPowerUp player)
        {
            throw new System.NotImplementedException();    
        }

        public override void RemoveEffects(PlayerForPowerUp player)
        {
            throw new System.NotImplementedException();
        }

        public override PowerUp Clone()
        {
            throw new System.NotImplementedException();
        }
    
    }
}
