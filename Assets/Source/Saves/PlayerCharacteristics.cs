using System;
using UnityEngine;

namespace Saves
{
    [Serializable]
    public class PlayerCharacteristics : IReadOnlyCharacteristics
    {
        public PlayerCharacteristics(int moneyCount, int energyCount, int maxEnergyCount, int passedLevelsCount, float speed,
            float masterVolume, float musicVolume, float additionalHealth, float resistance, int water)
        {
            MoneyCount = moneyCount;
            EnergyCount = energyCount;
            MaxEnergyCount = maxEnergyCount;
            PassedLevelsCount = passedLevelsCount;
            Speed = speed;
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
            Health = additionalHealth;
            Resistance = resistance;
            Water = water;
        }

        [field: SerializeField] public int MoneyCount { get; set; }
        [field: SerializeField] public int EnergyCount { get; set; }
        [field: SerializeField] public int MaxEnergyCount { get; set; }
        [field: SerializeField] public int PassedLevelsCount { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public float Resistance { get; set; }
        [field: SerializeField] public int Water { get; set; }
        [field: SerializeField] public float MasterVolume { get; set; }
        [field: SerializeField] public float MusicVolume { get; set; }
    }
}