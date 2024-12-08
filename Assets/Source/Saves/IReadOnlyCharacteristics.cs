namespace Saves
{
    public interface IReadOnlyCharacteristics
    {
        public int MoneyCount { get; }
        public int EnergyCount{ get; }
        public int MaxEnergyCount { get; }
        public int PassedLevelsCount{ get; }
        public float MasterVolume{ get; }
        public float MusicVolume { get; }
        public float Speed{ get; }
        public float Health{ get; }
        public int Water{ get; }
        public float Resistance{ get; }
    }
}