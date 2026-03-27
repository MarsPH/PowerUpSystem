namespace PowerUpSystem.Scripts
{
    public readonly struct PowerUpSlotData
    {
        public bool HasPowerUp { get; }
        public string Name { get; }
        public float Duration { get; }
        public bool IsSelected { get; }

        public PowerUpSlotData(bool hasPowerUp, string name, float duration, bool isSelected)
        {
            HasPowerUp = hasPowerUp;
            Name = name;
            Duration = duration;
            IsSelected = isSelected;
        }
    }
}
