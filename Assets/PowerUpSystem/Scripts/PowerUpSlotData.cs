namespace PowerUpSystem.Scripts
{
    public readonly struct PowerUpSlotData
    {
        public bool HasPowerUp { get; }
        public string Name { get; }
        public float Duration { get; }
        public bool IsSelected { get; }
        /// <summary>Set when the item has a known type (for icon lookup). Null for empty slots or custom power-ups.</summary>
        public PowerUpType? UiType { get; }

        public PowerUpSlotData(bool hasPowerUp, string name, float duration, bool isSelected, PowerUpType? uiType = null)
        {
            HasPowerUp = hasPowerUp;
            Name = name;
            Duration = duration;
            IsSelected = isSelected;
            UiType = uiType;
        }
    }
}
