using System;
using Saves;
using UnityEngine;

namespace Menu
{
    public class Progress
    {
        private readonly PlayerCharacteristics Characteristics;
        private readonly SaveService SaveService;

        public Progress(PlayerCharacteristics characteristics)
        {
            SaveService = new SaveService();
            Characteristics = SaveService.Load() ?? characteristics;
        }
        
        public Progress(SaveService saveService)
        {
            SaveService = saveService;
            Characteristics = SaveService.Load();
        }

        public event Action<IReadOnlyCharacteristics> Loaded; 
        public event Action<int> MoneyChanged; 
        public event Action<int, int> EnergyReceived; 
        public event Action<int> LevelIncreased; 

        public void Save()
        {
            Debug.Log(JsonUtility.ToJson(Characteristics));
            SaveService.Save(Characteristics);
        }

        public void Load()
        {
            Loaded?.Invoke(Characteristics);
        }
        
        public void ChangeMoney(int moneyCount)
        {
            Characteristics.MoneyCount += moneyCount;
            MoneyChanged?.Invoke(Characteristics.MoneyCount);
        }
        
        public void ChangeEnergy(int energyCount)
        {
            Characteristics.EnergyCount = energyCount;

            if (Characteristics.EnergyCount > Characteristics.MaxEnergyCount)
                Characteristics.EnergyCount = Characteristics.MaxEnergyCount;
            
            EnergyReceived?.Invoke(Characteristics.EnergyCount, Characteristics.MaxEnergyCount);
        }
        
        public void IncreaseLevel()
        {
            Characteristics.PassedLevelsCount++;
            LevelIncreased?.Invoke(Characteristics.PassedLevelsCount);
        }

        public void ChangeMasterVolume(float volume)
        {
            Characteristics.MasterVolume = volume;
        }
        
        public void ChangeMusicVolume(float volume)
        {
            Characteristics.MusicVolume = volume;
        }

        public void SetSpeed(object value)
        {
            Characteristics.Speed = (float)value;
        }

        public void SetHealth(object value)
        {
            Characteristics.Health = (float)value;
        }

        public void SetResistance(object value)
        {
            Characteristics.Resistance = (float)value;
        }
        
        public void SetWater(object value)
        {
            Characteristics.Water = (int)value;
        }
    }
}